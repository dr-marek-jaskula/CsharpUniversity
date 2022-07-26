using ASP.NETCoreWebAPI.Authentication;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Models.QueryObjects;
using ASP.NETCoreWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPI.Controllers;

//0. Authorization constants (like policy names and claim names I have moved to the "AuthorizationPolicyStaticClasses" for code clarity)

//1. Authorization is done by jwt token
//2. Registered user needs to log in by: "Account/login" and send in the request body the email and password (specified in LoginDto)
//3. The response will contain the jwt token like "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiUGF3ZWxDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkN1c3RvbWVyIiwiZXhwIjoxNjUwMjg1OTc5LCJpc3MiOiJodHRwOi8vQ3NoYXJwVW5pdmVyc2l0eVdlYkFwaS5jb20iLCJhdWQiOiJodHRwOi8vQ3NoYXJwVW5pdmVyc2l0eVdlYkFwaS5jb20ifQ.mBsPF9UuOlxlFxn1Ea9OT0wLqGZHjOqRl4jETqqObIk"
//4. To authorize we need to send the Header "Authorization" with value of given authorization method that is "Bearer {jwt token}" where {jwt token} is the token from previous step

[ApiController]
[Route("api/[controller]")]
//To use this controller Authorization is required unless "AllowAnonymous" attribute is specified
[Authorize]
//This attribute will change in the swagger that produces responses are in "application/json" format
[Produces("application/json")]
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
    [Authorize(Policy = MyAuthorizationPolicy.HasNationality)]
    [Authorize(Policy = MyAuthorizationPolicy.Mature)]
    public ActionResult<OrderDto> GetAuthorizeWithRequirements([FromRoute] int id)
    {
        var order = _orderService.GetById(id);
        return Ok(order);
    }

    [HttpGet("AuthorizeClaims/{id}")]
    //Authorization is required: based on the claims that needed to be satisfied
    [Authorize(Roles = $"{ClaimRoles.Administrator},{ClaimRoles.Manager}")]
    public ActionResult<OrderDto> GetAuthorizeWithClaims([FromRoute] int id)
    {
        var order = _orderService.GetById(id);
        return Ok(order);
    }

    //Authorization is required even it is not specified on the action level - it is specified on the controller level
    //Delete a record without querying it
    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        _orderService.Delete(id);
        return NoContent();
    }

    //Authorization is required even it is not specified on the action level - it is specified on the controller level
    //BulkUpdate with "linq2db.EntityFrameworkCore" (updating many record with one statement, which cannot be done by simple linq -> only raw sql or this package)
    [HttpPatch("{addAmount}")]
    public ActionResult BulkUpdate([FromRoute] int addAmount)
    {
        //We can also create bulk deletes
        _orderService.BulkUpdate(addAmount);
        return NoContent();
    }

    [HttpPost("[action]")]
    public ActionResult Create([FromBody] CreateOrderDto dto)
    {
        return Created($"/api/order/{2}", null);
    }

    //There is another, dynamic policy here: ResourceOperationRequirementHandler: if the resource was created by the user, then user can modify it, otherwise it is forbidden for user
    [HttpPut("{id}")]
    //Authorization is required: based on the requirements that are needed to be satisfied
    [Authorize(Policy = MyAuthorizationPolicy.CreateAtLeastTwoOrders)]
    public async Task<IActionResult> Update([FromBody] UpdateOrderDto dto, [FromRoute] int id)
    {
        //"User" here is the object with claims, that is required for the authorization
        await _orderService.Update(id, dto);
        return Ok();
    }

    //Pagination region

    //filtering is done on the name that is specified by the request parameter
    [HttpGet("{name}", Name = "GetOrderByName")]
    [AllowAnonymous]
    public async Task<ActionResult<OrderDto>> GetByName([FromRoute] string name)
    {
        var orderDto = await _orderService.GetByName(name);
        return Ok(orderDto);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll([FromQuery] OrderQuery query)
    {
        var orderDtos = await _orderService.GetAll(query);
        return Ok(orderDtos);
    }
}