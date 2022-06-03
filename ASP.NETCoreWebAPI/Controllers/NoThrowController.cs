using ASP.NETCoreWebAPI.Exceptions;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Services;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPI.Controllers;

//This controller demonstrated the way to avoid throwing exceptions and catching them in the middleware
//Exceptions can have bad influence on the performance of our application

//1. Install NuGet Package: LanguageExt.Core

//Details are covered in the NoThrowService and the extension method for the controller is place below.
//However, in real-life application it should be placed in a separate file, for instance in subfolder of Controllers called "ControllerExtensions".

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class NoThrowController : ControllerBase
{
    private readonly INoThrowService _noThrowService;

    public NoThrowController(INoThrowService noThrowService)
    {
        _noThrowService = noThrowService;
    }

    [HttpGet("{id}")]
    public ActionResult<CustomerDto> GetWithNoThrow([FromRoute] int id)
    {
        var customer = _noThrowService.GetById(id);
        return (ActionResult)customer.ToOk(c => c);
    }
}

//Extensions for api responses.
public static class ControllerExtensions
{
    //Performance is obtained but code is more complicated and need more extension method for different cases.
    //Moreover, the ProblemDetails are not returned (it should be additionally handled)
    //Also if other exceptions occur, they are need to be handled separately as here.
    //Approach is good for heavily occupied endpoints that should be optimized if possible
    public static IActionResult ToOk<TResult, TContract>(this Result<TResult> result, Func<TResult, TContract> mapper)
    {
        return result.Match<IActionResult>(obj =>
        {
            var response = mapper(obj);
            return new OkObjectResult(response);
        }, exception =>
        {
            if (exception is NotFoundException foundException)
                return new BadRequestObjectResult(foundException);

            return new StatusCodeResult(500);
        });
    }
}