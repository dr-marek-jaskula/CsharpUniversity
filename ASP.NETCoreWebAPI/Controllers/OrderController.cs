using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
//To use this controller Authorization is required unless "AllowAnonymous" attribute is specified
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    //The attributes like:
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //will not be included just for clarity (for tutorial purposes, else they would be present)

    [HttpGet("Anonymous/{id}")]
    //Allow unauthorized user to reach this endpoint
    [AllowAnonymous]
    public ActionResult<OrderDto> GetAnonymous([FromRoute] int id)
    {
        var order = _orderService.GetById(id);
        return Ok(order);
    }

    [HttpGet("Authorize/{id}")]
    //Authorization is required. It can be skipped because the controller required authorization
    [Authorize]
    public ActionResult<OrderDto> GetAuthorize([FromRoute] int id)
    {
        var order = _orderService.GetById(id);
        return Ok(order);
    }

    [HttpGet("AuthorizeRequirements/{id}")]
    //Authorization is required: based on the requirements that are needed to be satisfied
    [Authorize(Policy = "HasNationality")]
    [Authorize(Policy = "AtLeast18")]
    public ActionResult<OrderDto> GetAuthorizeWithRequirements([FromRoute] int id)
    {
        var order = _orderService.GetById(id);
        return Ok(order);
    }

    [HttpGet("AuthorizeClaims/{id}")]
    //Authorization is required: based on the claims that needed to be satisfied
    [Authorize(Roles = "Administrator,Manager")]
    public ActionResult<OrderDto> GetAuthorizeWithClaims([FromRoute] int id)
    {
        var order = _orderService.GetById(id);
        return Ok(order);
    }

    [HttpPost("[action]")]
    public ActionResult CreateOrder([FromBody] CreateOrderDto dto)
    {
        return Created($"/api/restaurant/{2}", null);
    }

    //Authorization is required even it is not specified on the action level - it is specified on the controller level
    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        //_restaurantService.Delete(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    //Authorization is required: based on the requirements that are needed to be satisfied
    [Authorize(Policy = "CreatedAtLeastTwoOrders")]
    public ActionResult Update([FromBody] UpdateOrderDto dto, [FromRoute] int id)
    {
        //_restaurantService.Update(id, dto);
        return Ok(); //or Update
    }
}