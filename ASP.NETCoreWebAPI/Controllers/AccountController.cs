using ASP.NETCoreWebAPI.Exceptions;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPI.Controllers;

//[controller] in Rount attribute is equivalent to the name of the controller (i.e. "Account")
[Route("api/[controller]")] 
[ApiVersion("1.0")] //states about version of this api controller 
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        //Inject service
        _accountService = accountService;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)] //informs about possible response
    [ProducesResponseType(StatusCodes.Status400BadRequest)] //informs about possible response
    public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
    {
        //Use injected service to inverse the dependency
        _accountService.RegisterUser(dto);
        return Ok();
    }

    //[action] in HttpPost attribute is equivalent to the name of the method
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))] //informs about possible response and states that the return type of action is string
    [ProducesResponseType(StatusCodes.Status400BadRequest)] //informs about possible response
    public ActionResult Login([FromBody] LoginDto dto)
    {
        string token = _accountService.GenerateJwt(dto);
        return Ok(token);
    }
}
