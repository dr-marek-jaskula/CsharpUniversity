using ASP.NETCoreWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GitHubDecoratedServiceController : ControllerBase
{
    private readonly IDecoratedGitHubService _gitHubService;

    public GitHubDecoratedServiceController(IDecoratedGitHubService gitHubService)
    {
        _gitHubService = gitHubService;
    }

    [HttpGet("users/cache/1/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePollyCache(string userName)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncCachePolly(userName);
        return user is not null ? Ok(user) : NotFound();
    }

    [HttpGet("users/cache/2/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePollyCache2(string userName)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncCachePolly2(userName);
        return user is not null ? Ok(user) : NotFound();
    }
}