using ASP.NETCoreWebAPI.Logging;
using ASP.NETCoreWebAPI.Models;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Models.QueryObjects;

namespace ASP.NETCoreWebAPI.Services.Decorators;

//This class decorates the OrderService class
//The point is to add the functionality of measuring the request time, without modifying the service.
//The decorator is registered using "Scrutor" package in the "Registration" folder "DecoratorsRegistration"

public class OrderServiceDecorator : IOrderService
{
    //The most important is to inject the service we decorate and implement it in the same time
    private readonly IOrderService _orderService;

    //logger is just to add logic we need
    private readonly ILoggerAdapter<IOrderService> _logger;

    public OrderServiceDecorator(IOrderService orderService, ILoggerAdapter<IOrderService> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    //This will work as follows:
    //1. When we ask the DI Container for the IOrderService, we get OrderServiceDecorator
    //2. But the OrderServiceDecorator will ask for IOrderService and this time we get OrderService (we can register multiple time same, but usually we get the last one)

    //Each method here just add some decorator logic and then calls the injected IOrderService

    public OrderDto GetById(int id)
    {
        //decorator logic
        using var _ = _logger.TimedOperation("Order retrieval for order id: {0}, ", id);
        //The service logic
        return _orderService.GetById(id);
    }

    public async Task Update(int id, UpdateOrderDto dto)
    {
        using var _ = _logger.TimedOperation("Order update for id: {0}, ", id);
        await _orderService.Update(id, dto);
    }

    public async Task<OrderDto> GetByName(string name)
    {
        using var _ = _logger.TimedOperation("Weather retrieval for order name: {0}, ", name);
        return await _orderService.GetByName(name);
    }

    public async Task<PageResult<OrderDto>> GetAll(OrderQuery query)
    {
        using var _ = _logger.TimedOperation("Order retrieval for search phase: {0} and sort direction ", query.SearchPhrase, query.SortDirection);
        return await _orderService.GetAll(query);
    }

    public void Delete(int id)
    {
        using var _ = _logger.TimedOperation("Order delete for id: {0}", id);
        _orderService.Delete(id);
    }

    public void BulkUpdate(int addAmount)
    {
        using var _ = _logger.TimedOperation("Order bulk update for addAmount: {0}", addAmount);
        _orderService.BulkUpdate(addAmount);
    }
}