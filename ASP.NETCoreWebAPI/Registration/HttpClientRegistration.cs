using ASP.NETCoreWebAPI.Models;
using Microsoft.Net.Http.Headers;
using Refit;

namespace Microsoft.Extensions.DependencyInjection;

public static class HttpClientRegistration
{
    public static void RegisterHttpClient(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddHttpClient("GitHub", client =>
        {
            //Headers required by GitHub
            //Base address
            client.BaseAddress = new Uri(config.GetValue<string>("GitHub:ApiBaseUrl"));
            //GitHub API versioning
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
            //GitHub requires a user-agent
            client.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpClientFactory-Sample");
        });

        //We need to Refit and Refit.HttpClientFactory for it to work
        services
            .AddRefitClient<IGitHubApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(config.GetValue<string>("GitHub:ApiBaseUrl")));
    }
}

//For Refit approach (here for demo purposes)
//Approach is only for RestfulApis

[Headers("Content-Type: application/json")]
//Authorization on the interface level:
//[Headers("Authorization: Bearer")]
public interface IGitHubApi
{
    [Get("/users/{user}")]
    [Headers("User-Agent:HttpClientFactory-Sample")]
    //This ApiResponse is to get full data about response and obtain more control 
    Task<ApiResponse<GitHubUser?>> GetUser(string user);

    //[Get("/users")]
    //Task<IEnumerable<GitHubUser>> GetAll();

    //[Post("/users")]
    //Task<GitHubUser> CreateUser([Body] GitHubUser user);

    //[Put("/users/{id}")]
    //Task<GitHubUser> UpdateUser(int id, [Body] GitHubUser user);

    //[Delete("/users/{id}")]
    //Task DeleteUser(int id);

    //For aliases:
    //[Get("/group/{id}/users")]
    //Task<List<GitHubUser>> GroupList([AliasAs("id")] int groupId);

    //For authorization at the method level:
    //[Get("/api/student/{studentId}")]
    //[Headers("User-Agent:StudentService")]
    //Task<Student> GetStudent(int studentId, [Authorize("Bearer")] string token);
}