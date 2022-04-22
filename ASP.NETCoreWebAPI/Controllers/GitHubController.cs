using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GitHubController : ControllerBase
{
    private readonly IGitHubService _gitHubService;

    public GitHubController(IGitHubService gitHubService)
    {
        _gitHubService = gitHubService;
    }

    #region Retry

    [HttpGet("users/retry/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePolly(string userName)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncPollyRetry(userName);
        return user is not null ? Ok(user) : NotFound();
    }

    #endregion Retry

    #region CircuitBreaker

    [HttpGet("users/cb/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePollyCB(string userName)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncPollyCB(userName);
        return user is not null ? (IActionResult)Ok(user) : NotFound();
    }

    [HttpGet("users/cb/advanced/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePollyCBAdvanced(string userName)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncPollyAdvanceCB(userName);
        return user is not null ? (IActionResult)Ok(user) : NotFound();
    }

    #endregion CircuitBreaker

    #region Timeout policy

    [HttpGet("users/timeout/1/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePollyTimeout(string userName)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncTimeoutPolly(userName);
        return user is not null ? (IActionResult)Ok(user) : NotFound();
    }

    [HttpGet("users/timeout/2/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePollyTimeout2(string userName)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncTimeoutPolly2(userName);
        return user is not null ? (IActionResult)Ok(user) : NotFound();
    }

    [HttpGet("users/timeout/3/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePollyTimeout3(string userName, CancellationToken token)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncTimeoutPolly3(userName, token);
        return user is not null ? (IActionResult)Ok(user) : NotFound();
    }

    [HttpGet("users/timeout/4/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePollyTimeout4(string userName)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncTimeoutPolly4(userName);
        return user is not null ? (IActionResult)Ok(user) : NotFound();
    }

    #endregion Timeout policy

    #region Cache policy

    [HttpGet("users/cache/1/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePollyCache(string userName)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncCachePolly(userName);
        return user is not null ? (IActionResult)Ok(user) : NotFound();
    }

    [HttpGet("users/cache/2/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePollyCache2(string userName)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncCachePolly2(userName);
        return user is not null ? (IActionResult)Ok(user) : NotFound();
    }

    [HttpGet("users/cache/3/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePollyCache3(string userName)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncCachePolly3(userName);
        return user is not null ? (IActionResult)Ok(user) : NotFound();
    }

    #endregion Cache policy

    #region Fallback policy

    [HttpGet("users/fallback/1/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePollyFallback(string userName)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncFallbackPolly(userName);
        return user is not null ? (IActionResult)Ok(user) : NotFound();
    }

    [HttpGet("users/fallback/2/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePollyFallback2(string userName)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncFallbackPolly2(userName);
        return user is not null ? (IActionResult)Ok(user) : NotFound();
    }

    #endregion Fallback policy

    #region Wrap multiple policies

    [HttpGet("users/wrap/1/{userName}")]
    public async Task<IActionResult> GetUserByUsernamePollyWrap(string userName, CancellationToken token)
    {
        var user = await _gitHubService.GetUserByUserNameAsyncWrapMultiplePolicies(userName, token);
        return user is not null ? (IActionResult)Ok(user) : NotFound();
    }

    #endregion Wrap multiple policies
}