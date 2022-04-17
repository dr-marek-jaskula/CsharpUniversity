using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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
    public IActionResult ConditionsInRoute()
    {
        //Other status code we return
        return NotFound();
    }

    //As an return value we can specify ActionResult or IActionResult

    //We can give route a name. Two times writing [HttpGet] is due to the Swagger issue.
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

    //1. FromBody: the request must include JSON object in the request body
    [HttpPost("[action]")]
    public ActionResult Create([FromBody] CreateOrderDto dto)
    {
        return Created($"/api/create/best", null);
    }

    //2. FromRoute: the request route will be used as a action parameter
    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        return NoContent();
    }

    //3. FromQuery: the request must specify an parameter for example "https://localhost:7240/api/University/GetFromQuery?myPageNumber=21&api-version=1.0"
    [HttpGet]
    [Route("[action]")]
    public IActionResult GetFromQuery([FromQuery(Name = "myBestPageNumber")] int myPageNumber) //We can also change the name or the parameter for the request
    {
        return Ok(myPageNumber);
    }

    //4. FromHeader: the request header "CreditCard" value will be used
    [HttpGet]
    [Route("[action]")]
    public IActionResult GetFromHeader([FromHeader(Name = "CreditCard")] string creditCardNumber)
    {
        //curl -X 'GET' \
        //'https://localhost:7240/api/University/GetFromHeader?api-version=1.0' \
        //  -H 'accept: */*' \
        //  -H 'CreditCard: 52436673'
        return Ok(creditCardNumber);
    }

    //5. FromForm: look at "FileController"

    //Other functionalities

    //No emphasize that a method in the controller is not an action we use [NotAction] attribute
    [NonAction]
    public void ThisIsNotAnAction()
    {
    }

    [HttpGet]
    [Route("swaggerIgnore")]
    //To ignore the action by swagger we use "ApiExplorerSettings" attribute and IngoreApi = true. Nevertheless, the endpoint is still valid
    [ApiExplorerSettings(IgnoreApi = true)]
    public ActionResult IgnoredAction()
    {
        return Ok("This is ignored by swagger but it is still a valid endpoint");
    }
}