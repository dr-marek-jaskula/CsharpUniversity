using ASP.NETCoreWebAPI.Authentication;
using ASP.NETCoreWebAPI.Exceptions;
using ASP.NETCoreWebAPI.Models;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Models.QueryObjects;
using ASP.NETCoreWebAPI.PollyPolicies;
using AutoMapper;
using EFCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Enums;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Security.Claims;

namespace ASP.NETCoreWebAPI.Services;

public interface IOrderService
{
    OrderDto GetById(int id);

    Task<OrderDto> GetByName(string name);

    //PageResult<OrderDto> GetAll(OrderQuery query);

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
    public Task<OrderDto> GetByName(string name)
    {
        //string approximatedName = Helpers.ApproximateName(name, _context.Champions);
        string approximatedName = name;

        return await((AsyncPolicy)PollyRegister.registry["AsyncCacheStrategy"]).ExecuteAsync(async context =>
       {
           if (approximatedName is "")
               throw new NotFoundException("Order not found");

           var order = await _dbContex.Orders
                .AsNoTracking() //to improve performance, because they are read only
                .Include(o => o.Payment)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.Product != null && (o.Product.Name.Contains(name) || o.Product.Name.Contains(approximatedName)));

           var result = _mapper.Map<OrderDto>(order);

           return result;
       }, new Context($"{approximatedName}"));
    }
}