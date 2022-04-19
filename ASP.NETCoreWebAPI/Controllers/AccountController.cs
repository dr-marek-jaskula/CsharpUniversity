using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPI.Controllers;

//Down below are example user accounts
//Authentication and authorization is done in OrderController

[ApiController]
//Describes which version the controller needs to be mapped for, the version is specified in the route
[ApiVersion("1.0")]
//Another supported version. Deprecated informs about obsolete versions
[ApiVersion("1.1", Deprecated = true)] //Deprecated versions in swagger make action crossed by line
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
    [MapToApiVersion("1.0")] //for swagger purposes, for api by default its "1.0" but swagger does not read default api version and is confused (crashes) when it is not applied
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
    [MapToApiVersion("1.0")]
    public ActionResult Login([FromBody] LoginDto dto) //If we would not specify [FromBody] is would be specified by default
    {
        string token = _accountService.GenerateJwt(dto);
        return Ok(token);
    }

    //Can get by:
    //https://localhost:7240/api/Account/VersionTest
    //https://localhost:7240/api/v1/Account/VersionTest
    //https://localhost:7240/api/v1.0/Account/VersionTest
    //https://localhost:7240/api/account/VersionTest/?api-version=1.0    (if it QueryStringApiVersionReader is specified as default)
    /// <summary>
    /// This is version 1 test method
    /// </summary>
    /// <returns></returns>
    [HttpGet("VersionTest")]
    [MapToApiVersion("1.0")]
    public ActionResult VersionTestActionV1()
    {
        return Ok("Version 1");
    }

    /// <summary>
    /// This is version 1.1. test method
    /// </summary>
    /// <returns></returns>
    //Can get by:
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

/* User Accounts (I have changed in a database binding to the customers/employees manually)

 * RoleId = 1 (Customer)
  "username": "PawelCustomer",
  "email": "pawel123@gmail.com",
  "password": "myPassword123",
  "confirmPassword": "myPassword123"

 * RoleId = 1 (Customer)
  "username": "MichalCustomer",
  "email": "michalCustomer123@gmail.com",
  "password": "Customer123",
  "confirmPassword": "Customer123"

 * RoleId = 2 (Employee)
  "username": "PawelEmployee",
  "email": "pawel321@gmail.com",
  "password": "myPassword321",
  "confirmPassword": "myPassword321",
  "RoleId": 2

 * RoleId = 3 (Manager)
  "username": "MichalManager",
  "email": "michal123@gmail.com",
  "password": "michalPassword123",
  "confirmPassword": "michalPassword123",
  "RoleId": 3

 * RoleId = 4 (Administrator)
  "username": "MichalAdministrator",
  "email": "michal321@gmail.com",
  "password": "michalPassword321",
  "confirmPassword": "michalPassword321",
  "RoleId": 4

*/