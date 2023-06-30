using ASP.NETCoreWebAPI.Models;
using ASP.NETCoreWebAPI.PollyPolicies;
using Microsoft.Net.Http.Headers;
using Polly;
using Polly.Registry;
using Refit;

namespace Microsoft.Extensions.DependencyInjection;

//In order to apply Polly Polities on the client level, we add "Microsoft.Extensions.Http.Polly" NuGet Package
//Then, after "AddHttpClient" we use one of the many possible method. My preferred way is to register policies from PolicyRegistry 

public static class HttpClientRegistration
{
    public static void RegisterHttpClient(this IServiceCollection services, ConfigurationManager config)
    {
        //This is a "named" client (here name is "GitHub"). I prefer this approach (but with client names in some static class as constants)
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
        //To bind the Polly Policy directly to the client we can use:
        //.AddPolicyHandler((AsyncPolicy<HttpResponseMessage>)PollyRegistry.asyncRegistry["CircuitBreakerStrategy3"]);

        //We can also use a "typed" client (we would need to create a client class for it)
        //This approach would require us to inject the HttpClient (and if the services are matching this client will be obtained)
        //services.AddHttpClient<IGitHubService, GitHubService>(client =>
        //{
        //    //Headers required by GitHub
        //    //Base address
        //    client.BaseAddress = new Uri(config.GetValue<string>("GitHub:ApiBaseUrl"));
        //    //GitHub API versioning
        //    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
        //    //GitHub requires a user-agent
        //    client.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpClientFactory-Sample");
        //}) //these two configuration handle problem when we inject transient typed client into singleton service
        //.ConfigurePrimaryHttpMessageHandler(() =>
        //{
        //    return new SocketsHttpHandler
        //    {
        //        PooledConnectionLifetime = TimeSpan.FromMinutes(5)
        //    };
        //})
        //.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        //We need to Refit and Refit.HttpClientFactory for this to work
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