using ASP.NETCoreWebAPI.Attributes;
using ASP.NETCoreWebAPI.Filters;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using Customers.Api.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResolvingDeps.WebApi.Attributes;

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

    //Basics about the request
    [HttpPut("RequestInfo")]
    [AllowAnonymous]
    public IActionResult FullInfoAboutRequest([FromBody] string inputString)
    {
        //Used Method (in this case PUT)
        Console.WriteLine(Request.Method);
        //Used Scheme (in this case https)
        Console.WriteLine(Request.Scheme);
        //Used ContentType (in this case application/json)
        Console.WriteLine(Request.ContentType);
        //Used ContentLenght (length of the string)
        Console.WriteLine(Request.ContentLength);
        //Used Host (in this case localhost:7240)
        Console.WriteLine(Request.Host);
        //Used Route (in this case /api/University/RequestInfo)
        Console.WriteLine(Request.Path);
        //Used query - dictionary (key-value pairs)
        //Console.WriteLine(Request.Query);
        //Used query (in this case ?api-version=1.0)
        Console.WriteLine(Request.QueryString.Value);
        //Console.WriteLine(Request.Headers); //Data about Headers
        return Ok();
    }

    //Determine what request is binded to this action. We can specify the route in the Http attribute, but its better to do this in a Route attribute
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
    //The id needs to be integer and not less than 1
    //Else, the 404 error will be returned
    [Route("minimalId/{id:int:min(1)}")]
    [AllowAnonymous]
    public IActionResult ConditionsInRoute()
    {
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
        //The Name property can be used for easier urls generating to certain action
        //For this particular one it would be:
        //string url = urlHelper.Link("NamedRoute", new { id = 5, query = "test" }); //this new { id = 5, query = "test" } is for tutorial values, but not for this action because it has no parameters
        //or if there is no urlHelper.
        //Url.Link("NamedRoute", null);
        //The second parameter is the action values
        return Ok();
    }

    //Preferred way for one route (IActionResult is a preferred way for a return type)
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

    //2. FromRoute: the request route last element will be used as a action parameter
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

    //6. FromMultiSource: this is custom attribute for multiple attributes for one object
    [HttpPut] //Use Put because Get cannot have a body
    [Route("[action]/{id:int}")]
    public IActionResult GetFromMultiSource([FromMultiSource] UpdateOrderRequest updateOrderRequest)
    {
        return Ok(updateOrderRequest);
    }

    //7. FromServices
    [HttpGet]
    [Route("injectServiceIntoAction")]
    //In some very rare cases we could inject the service directly to the method (mb for testing purposes).
    //It is uncommon to encounter
    //[FromServices] is required to tell that the dependency should be taken from the DI Container
    public ActionResult InjectToAction([FromServices] ILogger<UniversityController> logger)
    {
        return Ok($"{logger}");
    }

    //Other functionalities

    //To emphasize that a method (in the controller) is not an action we use [NotAction] attribute
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
        return Ok("This is ignored by swagger, but it is still a valid endpoint");
    }

    //Bad approach, because is hard to test (use the one below, but in some super rare cases we can use it like that)
    [HttpGet]
    [Route("actionWithAttrubute")]
    [DurationLogger]
    //This is an action to which we inject the dependency from the DI Container (DurationLogger)
    //The injection is done by the HttpContext in the attribute class
    public ActionResult InjectDependencyIntoAttribute()
    {
        return Ok("Use with double caution.");
    }

    [HttpGet]
    [Route("actionWithFilter")]
    [ServiceFilter(typeof(DurationLoggerFilter))]
    public ActionResult InjectDependencyIntoFilter()
    {
        return Ok("Use is in most cases if need functionality like in a action above.");
    }

    //Action with NameRoutingMiddleware (read-write Http context)
    [HttpGet]
    [Route("actionWithNameRouting/1")]
    public ActionResult NameRouting()
    {
        return Ok("Route: actionWithNameRouting/2 was be renamed to actionWithNameRouting/1");
    }

    //Actions with same name but they will be called if the "accept" header matches 
    //They crash swagger, and currently there are is no known solution for this problem.
    //Therefore, this code is commented (one will be left to see the usability)
    [HttpGet]
    [Route("sameRoute")]
    [AcceptHeader("*/*")]
    public ActionResult RouteWithAcceptHeaderSometingSlashSomething()
    {
        return Ok("Header: */*");
    }

    //[HttpGet]
    //[Route("sameRoute")]
    //[AcceptHeader("application/json")]
    //public ActionResult RouteWithAcceptHeaderApplicationSlashJson()
    //{
    //    return Ok("Header: application/json");
    //}

    //[HttpGet]
    //[Route("sameRoute")]
    //[AcceptHeader("text/xml")]
    //public ActionResult RouteWithAcceptHeaderTextSlashXml()
    //{
    //    return Ok("Header: text/xml");
    //}
}