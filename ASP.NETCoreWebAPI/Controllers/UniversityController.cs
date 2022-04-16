using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPI.Controllers;

//Inform program that this is a ApiController (can done globally for all controller but it is a bit uncomfortable)
[ApiController]
//Specify the controller route (url path)
//[controller] mens that this will accept "University" and "university" in the route
[Route("api/[controller]")]
public class UniversityController : ControllerBase
{
    //Here inject services, like logger, automapper and other. Examine other controllers to study this
    public UniversityController()
    {
    }

    //Basics about the Actions for Get HTTP verb (to obtain data)

    //Determine what request is binded to this action. We can specify the route in the Http attribute but its better to do this in a Route attribute
    [HttpGet]
    //It is possible to have two different routs that leads to the same action
    [Route("firstRoute")] // https://localhost:7240/api/Order/firstRoute
    [Route("secondRoute")] // https://localhost:7240/api/Order/secondRoute
    //[action] is equivalent to the method name
    [Route("[action]")] // https://localhost:7240/api/Order/TripleRouts
    public ActionResult TripleRouts()
    {
        //ActionResult return nothing but StatusCode
        //Status code Ok is 200
        return Ok();
    }

    [HttpGet]
    //We could define the conditions inside the route:
    //The id needs to be integer and not less then 1
    //Else, the 404 error will be returned
    [Route("minimalId/{id:int:min(1)}")]
    [AllowAnonymous]
    public ActionResult ConditionsInRoute()
    {
        //Other status code we return
        return NotFound();
    }

    //We can give route a name. Two times writting [HttpGet] is due to the Swagger issue.
    [HttpGet]
    [HttpGet("bestRoute", Name = "NamedRoute")]
    //We can give route a name in standard way
    [Route("[action]", Name = "[controller]_[action]")]
    //We can determine the order of the route (hierarchy)
    [Route("Home", Order = 2)]
    public ActionResult NamedRoutsAndOrderHierarchy()
    {
        return Ok();
    }

    [HttpGet("async")]
    public async Task<IActionResult> GetAsync()
    {
        await Task.Delay(1);
        return Ok();
    }

    //We can specify more request HTTP verbs (the most important are):
    //GET       - get object
    //POST      - create new objects (responses: CREATED or OK). Object can not exist
    //PUT       - replace all current object by incoming resource or create a new object (responses: CREATED or NO_CONTENT). Object does not need to exist
    //DELETE    - delete the object
    //PATCH     - partial modification of a object (responses: OK or NO_CONTENT). Object must exist

    //and others
    //HEAD      - asks for a response identical to a GET request, but without the response body.
    //OPTIONS   - describes the communication options for the target resource.
    //CONNECT   - establishes a tunnel to the server identified by the target resource.
    //TRACE     - performs a message loop-back test along the path to the target resource.

    //Parameter attributes:

    [HttpPost("[action]")]
    public ActionResult Create([FromBody] CreateOrderDto dto)
    {
        return Created($"/api/create/best", null);
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

    //Other

    //ProducesAttribute is used to specify the content type of the output that will be sent by the API.
    //The corresponding ConsumesAttribute is used to specify the request content types the API expects to receive

    //[Produces("application/json")]
    //[Consumes("application/json")]

    //The same of:
    //[Produces(MediaTypeNames.Application.Json)]
    //[Consumes(MediaTypeNames.Application.Json)]

    //[HttpGet]
    ////Inform user about return and request body
    //[Route("ProducesConsumes")]
    //[Produces(MediaTypeNames.Application.Json)]
    //[Consumes(MediaTypeNames.Application.Json)]
    //[AllowAnonymous]
    //public ActionResult GetProducesConumes([FromBody] int body)
    //{
    //    return Ok();
    //}

    //[ServiceFilter(typeof(ValidationFilterAttribute))]

    //[ResponseCache(CacheProfileName = "120SecondsDuration")]

    //[ApiExplorerSettings(GroupName = "v2")]
}