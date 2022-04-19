using ASP.NETCoreWebAPI.Authentication;
using ASP.NETCoreWebAPI.Exceptions;
using ASP.NETCoreWebAPI.Models;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Models.QueryObjects;
using AutoMapper;
using EFCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASP.NETCoreWebAPI.Services;

public interface IOrderService
{
    OrderDto GetById(int id);

    //PageResult<OrderDto> GetAll(OrderQuery query);

    //int Create(CreateOrderDto dto);

    //void Delete(int id);

    void Update(int id, UpdateOrderDto dto, ClaimsPrincipal user);
}

public class OrderService : IOrderService
{
    private readonly MyDbContext _dbContex;
    private readonly IMapper _mapper;

    //_authorizationService is for a dynamic requirements (in our case it is "ResourceOperationHandler")
    private readonly IAuthorizationService _authorizationService;

    //????????????????
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
    public void Update(int id, UpdateOrderDto dto, ClaimsPrincipal user)
    {
        var order = _dbContex.Orders
            .Include(o => o.Product)
            .Include(o => o.Payment)
            .FirstOrDefault(o => o.Id == id);

        //If the employee is a manager then return success, else fail the authorization
        var authorizationResult = _authorizationService.AuthorizeAsync(user, order, new ResourceOperationRequirement(ResourceOperation.Read)).Result;

        //If authorization fails, throw new ForbidException
        if (!authorizationResult.Succeeded)
            throw new ForbidException();

        if (order is null)
            throw new NotFoundException("Order not found");

        order.Amount = dto.Amount;

        if (dto.ProductId is not null)
        {
            order.ProductId = dto.ProductId;
        }

        _dbContex.SaveChanges();

        var order2 = _dbContex.Orders
            .Include(o => o.Product)
            .Include(o => o.Payment)
            .FirstOrDefault(o => o.Id == id);
    }
}