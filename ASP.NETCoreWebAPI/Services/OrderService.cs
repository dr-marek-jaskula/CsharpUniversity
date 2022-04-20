using ASP.NETCoreWebAPI.Authentication;
using ASP.NETCoreWebAPI.Exceptions;
using ASP.NETCoreWebAPI.Models;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Models.QueryObjects;
using ASP.NETCoreWebAPI.PollyPolicies;
using AutoMapper;
using EFCore;
using EFCore.Data_models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Enums;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Caching;
using System.Linq.Expressions;
using System.Security.Claims;

namespace ASP.NETCoreWebAPI.Services;

public interface IOrderService
{
    OrderDto GetById(int id);

    Task<OrderDto> GetByName(string name);

    Task<PageResult<OrderDto>> GetAll(OrderQuery query);

    //int Create(CreateOrderDto dto);

    //void Delete(int id);

    void Update(int id, UpdateOrderDto dto);
}

public class OrderService : IOrderService
{
    private readonly MyDbContext _dbContex;
    private readonly IMapper _mapper;

    //_authorizationService is for a dynamic requirements (in our case it is "ResourceOperationHandler")
    private readonly IAuthorizationService _authorizationService;

    //This is very flexible way to get the user data (custom made in Services). We inject if to have user information easy to get
    private readonly IUserContextService _userContextService;

    public OrderService(MyDbContext dbContex, IMapper mapper, ILogger<OrderService> logger, IAuthorizationService authorizationService, IUserContextService userContextService)
    {
        _dbContex = dbContex;
        _mapper = mapper;
        _authorizationService = authorizationService;
        _userContextService = userContextService;
    }

    public OrderDto GetById(int id)
    {
        var order = _dbContex.Orders
            .Include(o => o.Product)
            .Include(o => o.Payment)
            .FirstOrDefault(o => o.Id == id);

        if (order is null)
            throw new NotFoundException("Order not found");

        var result = _mapper.Map<OrderDto>(order);
        return result;
    }

    //Method with dynamic requirement "ResourceOperationRequirement"
    public void Update(int id, UpdateOrderDto dto)
    {
        var order = _dbContex.Orders
            .Include(o => o.Product)
            .Include(o => o.Payment)
            .FirstOrDefault(o => o.Id == id);

        //We get the user from the _userContextService, so the we apply the Dependency Inversion
        ClaimsPrincipal? user = _userContextService.User;

        if (user is null)
            throw new ForbidException("Unauthorized user");

        //If the employee is a manager then return success, else fail the authorization
        var authorizationResult = _authorizationService.AuthorizeAsync(user, order, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

        //If authorization fails, throw new ForbidException
        if (!authorizationResult.Succeeded)
            throw new ForbidException("User has no access to this order");

        if (order is null)
            throw new NotFoundException("Order not found");

        order.Amount = dto.Amount;

        if (dto.ProductId is not null)
            order.ProductId = dto.ProductId;

        _dbContex.SaveChanges();
    }

    //Filtering and Pagination with async programming

    /// <summary>
    /// Basic filtration
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<OrderDto> GetByName(string name)
    {
        //Approximation algorithm used to match the given name with product name list
        //BK-Tree algorithm (for a list overload) (we use Helpers namespace here)
        //string approximatedName = Helpers.Helpers.ApproximateNameByBKTree(name, GetAllUniqueProductNames());

        //SymSpell algorithm
        string approximatedName = name;

        //Get the Polly policy from the policy registry. The policy defines the caching strategy (policies are defined in PollyPolicies)
        AsyncPolicy pollyPolicy = (AsyncPolicy)PollyRegister.registry["AsyncCacheStrategy"];

        return await pollyPolicy.ExecuteAsync(async context =>
        {
            if (approximatedName is "")
                throw new NotFoundException("Order not found");

            var order = await _dbContex.Orders
                  .AsNoTracking() //to improve performance, because they are read only
                  .Include(o => o.Payment)
                  .Include(o => o.Product)
                  .FirstOrDefaultAsync(o => o.Product != null
                        && (o.Product.Name.ToLower().Contains(name.ToLower()) || o.Product.Name.ToLower().Contains(approximatedName.ToLower())));

            var result = _mapper.Map<OrderDto>(order);

            return result;
        }, new Context($"{approximatedName}")); //Context object determines the external data, that is passed to the pollyPolicy
    }

    public async Task<PageResult<OrderDto>> GetAll(OrderQuery query)
    {
        AsyncPolicy pollyPolicy = (AsyncPolicy)PollyRegister.registry["AsyncCacheStrategy"];

        return await pollyPolicy.ExecuteAsync(async context =>
        {
            var baseQuery = _dbContex.Orders
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
        return _dbContex.Products
            .AsNoTracking()
            .Select(p => p.Name)
            .Distinct()
            .ToList();
    }
}