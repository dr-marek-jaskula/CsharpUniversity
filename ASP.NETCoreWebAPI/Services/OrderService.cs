using ASP.NETCoreWebAPI.Exceptions;
using ASP.NETCoreWebAPI.Models;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Models.QueryObjects;
using AutoMapper;
using EFCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreWebAPI.Services;

public interface IOrderService
{
    OrderDto GetById(int id);

    //PageResult<OrderDto> GetAll(OrderQuery query);

    //int Create(CreateOrderDto dto);

    //void Delete(int id);

    //void Update(int id, UpdateOrderDto dto);
}

public class OrderService : IOrderService
{
    private readonly MyDbContext _dbContex;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authorizationService;
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
}