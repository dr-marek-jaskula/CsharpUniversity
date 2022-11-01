using ASP.NETCoreWebAPI.Authentication;
using ASP.NETCoreWebAPI.Exceptions;
using ASP.NETCoreWebAPI.Models;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Models.QueryObjects;
using ASP.NETCoreWebAPI.PollyPolicies;
using ASP.NETCoreWebAPI.StringApproxAlgorithms;
using AutoMapper;
using Bogus;
using EFCore;
using EFCore.Data_models;
using LinqToDB.Data;
using LinqToDB.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Enums;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Linq.Expressions;
using System.Security.Claims;
using Throw;

namespace ASP.NETCoreWebAPI.Services;

public interface IOrderService
{
    OrderDto GetById(int id);

    Task<OrderDto> GetByName(string name);

    Task<PageResult<OrderDto>> GetAll(OrderQuery query);

    //int Create(CreateOrderDto dto);

    //Delete without querying a record
    void Delete(int id);

    //Bulk Update -> update many records using single sql command which is not possible by simple linq (we use "linq2db.EntityFrameworkCore"
    void BulkUpdate(int addAmount);

    Task Update(int id, UpdateOrderDto dto);
}

public class OrderService : IOrderService
{
    private readonly MyDbContext _dbContext;
    private readonly IMapper _mapper;

    //SymSpells contain dictionary with at least key "en" with SymSpell instance with English frequency dictionary. After the first use of "GetByName" action, the "Products" key will be added and used
    private readonly SymSpells _symSpells;

    //_authorizationService is for a dynamic requirements (in our case it is "ResourceOperationHandler")
    private readonly IAuthorizationService _authorizationService;

    //This is very flexible way to get the user data (custom made in Services). We inject it to have user information easy to get
    private readonly IUserContextService _userContextService;

    public OrderService(MyDbContext dbContex, IMapper mapper, ILogger<OrderService> logger, IAuthorizationService authorizationService, IUserContextService userContextService, SymSpells symSpells)
    {
        _dbContext = dbContex;
        _mapper = mapper;
        _authorizationService = authorizationService;
        _userContextService = userContextService;
        _symSpells = symSpells;
    }

    public OrderDto GetById(int id)
    {
        var order = _dbContext.Orders
            .Find(id);
            //With eager loading this would look like:
            //.Include(o => o.Product)
            //    .ThenInclude(p => p == null ? null : p.Shops)
            //.Include(o => o.Product) //To get the Tags also
            //    .ThenInclude(p => p == null ? null : p.Tags)
            //.Include(o => o.Payment)
            //.FirstOrDefault(o => o.Id == id);

        if (order is null)
            throw new NotFoundException("Order not found");

        //Explicit loading (so explicitly determined lazy loading)
        _dbContext.Entry(order).Reference(o => o.Product).Query()
            .Include(p => p.Shops)
            .Include(p => p.Tags)
            //Splitting queries can be used to avoid the cartesian explosion problem. Nevertheless, in this case it decreases the performance (k6 load testing)
            //.AsSplitQuery() 
            .Load();

        _dbContext.Entry(order).Reference(o => o.Payment).Query()
            //In this case it decreases the performance. 
            //.AsSplitQuery()
            .Load();

        var result = _mapper.Map<OrderDto>(order);
        return result;
    }

    //Method with dynamic requirement "ResourceOperationRequirement"
    public async Task Update(int id, UpdateOrderDto dto)
    {
        //We get the user data from _userContextService
        ClaimsPrincipal? user = _userContextService.User;

        if (user is null)
            throw new ForbidException("Unauthorized user");

        //Explicit loading and Find method
        var order = _dbContext.Orders
            .Find(id);

        if (order is null)
            throw new NotFoundException("Order not found");

        _dbContext.Entry(order).Reference(o => o.Product).Query().Load();
        _dbContext.Entry(order).Reference(o => o.Payment).Query().Load();

        //If the employee is a manager, then return success, else fail the authorization
        var authorizationResult = await _authorizationService.AuthorizeAsync(user, order, new ResourceOperationRequirement(ResourceOperation.Update));

        //If authorization fails, throw new ForbidException
        if (!authorizationResult.Succeeded)
            throw new ForbidException("User has no access to this order");

        order.Amount = dto.Amount;

        if (dto.ProductId is not null)
            order.ProductId = dto.ProductId;

        _dbContext.SaveChanges();
    }

    //Filtering and Pagination with async programming

    /// <summary>
    /// Order filtration by name. String approximation and caching is ensured.
    /// </summary>
    /// <param name="name">Name of the product</param>
    /// <returns>User order</returns>
    public async Task<OrderDto> GetByName(string name)
    {
        //Approximation algorithm used to match the given name with product name list

        //BK-Tree algorithm
        //string approximatedName = BKTreeAlgorithm.ApproximateNameByBKTree(name, GetAllUniqueProductNames());

        //SymSpell algorithm for English dictionary
        //string approximatedName = SymSpellAlgorithm.FindBestSuggestion(name, _symSpellEnDictionary);

        //SymSpell algorithm for dictionary made of product name list
        _symSpells.SymSpellsDictionary.TryAdd("Products", SymSpellFactory.CreateSymSpell(GetAllUniqueProductNames()));
        string approximatedName = SymSpellAlgorithm.FindBestSuggestion(name, _symSpells.SymSpellsDictionary["Products"]);

        //Get the Polly policy from the policy registry. The policy defines the caching strategy (policies are defined in PollyPolicies)
        AsyncPolicy pollyPolicy = (AsyncPolicy)PollyRegistry.asyncRegistry["AsyncCacheStrategy"];

        return await pollyPolicy.ExecuteAsync(async context =>
        {
            var order = await _dbContext.Orders
                  .AsNoTracking() //to improve performance, because they are read only
                  .Include(o => o.Payment)
                  .Include(o => o.Product)
                  .FirstOrDefaultAsync(o => o.Product != null
                        && (o.Product.Name.ToLower().Contains(name.ToLower()) || o.Product.Name.ToLower().Contains(approximatedName.ToLower())));

            if (order is null)
                throw new NotFoundException("Order not found");

            var result = _mapper.Map<OrderDto>(order);

            return result;
        }, new Context($"{approximatedName}")); //Context object determines the external data, that is passed to the pollyPolicy
    }

    public async Task<PageResult<OrderDto>> GetAll(OrderQuery query)
    {
        AsyncPolicy pollyPolicy = (AsyncPolicy)PollyRegistry.asyncRegistry["AsyncCacheStrategy"];

        return await pollyPolicy.ExecuteAsync(async context =>
        {
            var baseQuery = _dbContext.Orders
                  .AsNoTracking()
                  .Include(o => o.Payment)
                  .Include(o => o.Product)
                  .Where(o => o.Product != null && (
                                query.SearchPhrase == null
                                || o.Product.Name.ToLower().Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                //Columns that can be use for sorting. At first nameof column that will be used for sorting (we can just write "Name" or "Amount") and then this property
                var columnsSelector = new Dictionary<string, Expression<Func<Order, object>>>
                {
                    { nameof(Order.Product.Name), o => o.Product != null ? o.Product.Name : ""},
                    { nameof(Order.Amount), o => o.Amount },
                };
                //Above we have boxing. To avoid it we can list the possible types (string, int and other if necessary) and create different Expressions for different types:
                /*
                Expression<Func<Order, string>> stringExpression;
                Expression<Func<Order, int>> intExpression;
                if (query.SortBy == nameof(Order.Product.Name))
                    stringExpression = o => o.Product != null ? o.Product.Name : "";
                if (query.SortBy == nameof(Order.Amount))
                    intExpression = o => o.Amount;
                */
                //Then, the below code should also be modified.

                var selectedColumn = columnsSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            //Pagination, skip some and take the number of items that equal to the PageSize
            var orders = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToListAsync();

            int totalItemsCount = baseQuery.Count();

            //Map order to dtos
            var ordersDtos = _mapper.Map<List<OrderDto>>(orders);

            //Pagination: map dtos to PageResults
            var result = new PageResult<OrderDto>(ordersDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }, new Context($"{query}"));
    }

    private List<string> GetAllUniqueProductNames()
    {
        return _dbContext.Products
            .AsNoTracking()
            .Select(p => p.Name)
            .Distinct()
            .ToList();
    }

    //When product names changes in the API runtime
    private void UpdateProductsSymSpellDictionary()
    {
        _symSpells.SymSpellsDictionary["Products"] = SymSpellFactory.CreateSymSpell(GetAllUniqueProductNames());
    }

    //Delete a record without querying it
    public void Delete(int id)
    {
        //At first we create a new Order with an id given by the user
        var orderToDelete = new Order() { Id = id };
        //Then we Attach this instance to the ChangeTracker and get the entry from it
        var entry = _dbContext.Orders.Attach(orderToDelete);
        //Next we change the entry status to "Delete"
        entry.State = EntityState.Deleted;
        //Finally we save changes
        try
        {
            _dbContext.SaveChanges();
        }
        catch
        {
            throw new NotFoundException($"Order with id = {id} not found");
        }
    }

    //Bulk Update -> update many records using single sql command which is not possible by simple linq (we use "linq2db.EntityFrameworkCore"
    public void BulkUpdate(int addAmount)
    {
        //1) Install NuGet Package "linq2db.EntityFrameworkCore"
        //2) Make IQueryable
        var orders = _dbContext.Orders
            //.AsQueryable() //If no filters or other, just use "AsQueryable"
            .Where(o => o.Status == Status.InProgress);

        //Use LinqToDB
        LinqToDB.LinqExtensions.Update(orders.ToLinqToDB(), o => new Order
        {
            Amount = o.Amount + addAmount,
        });
        //We do not need to call db.SaveChanges() because Linq to db will use it

        //Just one command will be used

        //The other way for Bulk Updated/Deletes is to use raw sql by for instance "FromSqlInterpolated" method
    }

    //Bulk Insert -> insert many records with better performance (we use "linq2db.EntityFrameworkCore")
    public async Task BulkInsert()
    {
        // Install NuGet Package "linq2db.EntityFrameworkCore"
        var addressFaker = new Faker<EFCore.Data_models.Address>()
            .RuleFor(a => a.Street, f => f.Address.StreetName())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.Country, f => f.Address.Country())
            .RuleFor(a => a.ZipCode, f => f.Address.ZipCode())
            .RuleFor(a => a.Building, f => f.Random.Bool(0.9f) ? f.Random.Int(1, 30) : null)
            .RuleFor(a => a.Flat, f => f.Random.Bool(0.9f) ? f.Random.Int(1, 90) : null);

        var addresses = addressFaker.Generate(10001);

        var options = new BulkCopyOptions
        {
            TableName = "Address"
        };

        await _dbContext.BulkCopyAsync(options, addresses);
    }

    //This is very important to pass to all async method like "ToListAsync" the cancellation token
    //Then, the query will be stopped also on the database side and not only on the client side
    //The async methods should be only used (for tutorial purpose they are not use everywhere), so cancellation token should be everywhere
    public async Task<List<Order>> GetOrdersWithCancellationToken(CancellationToken token)
    {
        //It is very important to pass the token
        return await _dbContext.Orders
            .ToListAsync(token);
    }

    //Great Helper method, generic. Example of use: "Order order = await GetOrder(3, db, o => o.Customer, o => o.Payment);"
    private async Task<Order> GetOrder(int orderId, MyDbContext db, params Expression<Func<Order, object?>>[] includes)
    {
        //Solution: create a IQueryable object
        var baseQuery = db.Orders
            .Where(o => o.Id == orderId);
        //.AsQueryable(); //if there is no method that will make is IQueryable

        //Include data that are required
        if (includes.Any())
            foreach (var include in includes)
                baseQuery = baseQuery.Include(include);

        //Materialize the result
        Order order = await baseQuery.FirstAsync();

        return order;
    }
}