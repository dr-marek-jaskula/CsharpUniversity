using ASP.NETCoreWebAPI.Models;

namespace ASP.NETCoreWebAPI.Services;

//This is an approach for Refit, to get fast and clean dealing with external Apis
//The interface IGitHubApi I have placed in "HttpClientRegistration" for demo purposes

public interface IRefitGitHubService
{
    Task<GitHubUser?> GetUserByUserNameRefit(string userName);
}

public class RefitGitHubService : IRefitGitHubService
{
    private readonly IGitHubApi _gitHubClient;

    public RefitGitHubService(IGitHubApi gitHubClient)
    {
        _gitHubClient = gitHubClient;
    }

    public async Task<GitHubUser?> GetUserByUserNameRefit(string userName)
    {
        var result = await _gitHubClient.GetUser(userName);

        if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        return result.Content;
    }
}