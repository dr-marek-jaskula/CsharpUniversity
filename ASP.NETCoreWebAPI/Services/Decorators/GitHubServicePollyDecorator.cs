using ASP.NETCoreWebAPI.Models;
using ASP.NETCoreWebAPI.PollyPolicies;
using Polly;

namespace ASP.NETCoreWebAPI.Services.Decorators;

public class GitHubServicePollyDecorator : IDecoratedGitHubService
{
    private readonly IDecoratedGitHubService _decoratedGitHubService;

    public GitHubServicePollyDecorator(IDecoratedGitHubService decoratedGitHubService)
    {
        _decoratedGitHubService = decoratedGitHubService;
    }

    public async Task<GitHubUser?> GetUserByUserNameAsyncCachePolly(string userName)
    {
        AsyncPolicy pollyPolicy = (AsyncPolicy)PollyRegistry.asyncRegistry["CacheStrategy"];

        return await pollyPolicy.ExecuteAsync(async context =>
        {
            return await _decoratedGitHubService.GetUserByUserNameAsyncCachePolly(userName);
        }, new Context("MyOperationalKey"));
    }

    public async Task<GitHubUser?> GetUserByUserNameAsyncCachePolly2(string userName)
    {
        AsyncPolicy pollyPolicy = (AsyncPolicy)PollyRegistry.asyncRegistry["CacheStrategy"];

        return await pollyPolicy.ExecuteAsync(async context =>
        {
            return await _decoratedGitHubService.GetUserByUserNameAsyncCachePolly2(userName);
        }, new Context(userName));
    }
}