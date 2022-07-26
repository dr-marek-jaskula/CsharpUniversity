using ASP.NETCoreWebAPI.Models;
using Newtonsoft.Json;
using System.Net;

namespace ASP.NETCoreWebAPI.Services;

public interface IDecoratedGitHubService
{
    Task<GitHubUser?> GetUserByUserNameAsyncCachePolly(string userName);

    Task<GitHubUser?> GetUserByUserNameAsyncCachePolly2(string userName);
}

public class DecoratedGitHubService : IDecoratedGitHubService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DecoratedGitHubService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<GitHubUser?> GetUserByUserNameAsyncCachePolly(string userName)
    {
        var client = _httpClientFactory.CreateClient("GitHub");

        var result = await client.GetAsync($"/users/{userName}");

        if (result.StatusCode is HttpStatusCode.NotFound)
            return null;

        var resultString = await result.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<GitHubUser>(resultString);
    }

    public async Task<GitHubUser?> GetUserByUserNameAsyncCachePolly2(string userName)
    {
        var client = _httpClientFactory.CreateClient("GitHub");

        var result = await client.GetAsync($"/users/{userName}");

        if (result.StatusCode is HttpStatusCode.NotFound)
            return null;

        var resultString = await result.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<GitHubUser>(resultString);
    }
}