using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPI.Controllers;

[ApiController]
//Describes which version the controller needs to be mapped for, the version is specified in the route
[ApiVersion("1.0")]
//Another supported version. Deprecated informs about obsolete versions
[ApiVersion("1.1", Deprecated = true)]
//"v{version:apiVersion}" adds the version number defined in the ApiVersion to the route while matching.
//In this case, both "/api/v1.0/account" and "/api/v1/account" are mapped to this controller. 
[Route("api/v{version:apiVersion}/[controller]")]
//[controller] in Route attribute is equivalent to the name of the controller (i.e. "Account" or "account")
[Route("api/[controller]")] // for backward compatibility (/api/account)
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


    //Can get by:
    //https://localhost:7240/api/Account/VersionTest
    //https://localhost:7240/api/v1/Account/VersionTest
    //https://localhost:7240/api/v1.0/Account/VersionTest
    //https://localhost:7240/api/account/VersionTest/?api-version=1.0    (if it QueryStringApiVersionReader is specified as default)
    [HttpGet("VersionTest")]
    public ActionResult VersionTestActionV1()
    {
        return Ok("Version 1");
    }

    ///Can get by:
    //https://localhost:7240/api/v1.1/Account/VersionTest
    //https://localhost:7240/api/account/VersionTest/?api-version=1.1    (if QueryStringApiVersionReader())
    //https://localhost:7240/api/account/VersionTest        (with header "api-version" set to "1.1".) (if HeaderApiVersionReader("api-version"))
    //https://localhost:7240/api/account/VersionTest        (with header "CustomHeaderVersion" set to "1.1".) (if HeaderApiVersionReader("CustomHeaderVersion"))
    [HttpGet("VersionTest")]
    [MapToApiVersion("1.1")]
    public ActionResult VersionTestActionV11()
    {
        return Ok("Version 1.1");
    }
}
