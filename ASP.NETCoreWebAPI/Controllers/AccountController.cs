using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPI.Controllers;

[ApiController]
//Describes which version the controller needs to be mapped for, the version is specified in the route
[ApiVersion("1.0")]
//Controller supports also 2.0 (because we have one 2.0 method in it)
[ApiVersion("2.0")]
//"v{version:apiVersion}" adds the version number defined in the ApiVersion to the route while matching.
//In this case, both "/api/v1.0/account" and "/api/v1/account" are mapped to this controller. (and for 2.0 also)
[Route("api/v{version:apiVersion}/[controller]")]
//[controller] in Route attribute is equivalent to the name of the controller (i.e. "Account")
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
    //https://localhost:7240/api/Account/VersionTestActionV1
    //https://localhost:7240/api/v1/Account/VersionTestActionV1
    //https://localhost:7240/api/v1.0/Account/VersionTestActionV1
    [HttpGet("VersionTest")]
    public ActionResult VersionTestActionV1()
    {
        return Ok("Version 1");
    }

    //Can get by:
    //https://localhost:7240/api/Account/VersionTestActionV2
    //https://localhost:7240/api/v2/Account/VersionTestActionV2
    //https://localhost:7240/api/v2.0/Account/VersionTestActionV2
    [HttpGet("VersionTest")]
    //Tell compiler that this method is for 2.0 version 
    [MapToApiVersion("2.0")]
    public ActionResult VersionTestActionV2() //one of the ways of underlining the change
    {
        return Ok("Version 2");
    }
}
