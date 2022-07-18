using ASP.NETCoreWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GitHubRefitServiceController : ControllerBase
{
    private readonly IRefitGitHubService _gitHubService;

    public GitHubRefitServiceController(IRefitGitHubService gitHubService)
    {
        _gitHubService = gitHubService;
    }

    [HttpGet("users/cache/1/{userName}")]
    public async Task<IActionResult> GetUserByUsernameRefite(string userName)
    {
        var user = await _gitHubService.GetUserByUserNameRefit(userName);
        return user is not null ? Ok(user) : NotFound();
    }
}