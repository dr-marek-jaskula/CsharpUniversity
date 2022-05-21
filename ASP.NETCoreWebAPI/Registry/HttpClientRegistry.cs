namespace ASP.NETCoreWebAPI.Registry;

public static class HttpClientRegistry
{
    public static void RegisterHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient("GitHub", client =>
        {
            //Headers required by GitHub
            //Base address
            client.BaseAddress = new Uri("https://api.github.com/");
            //GitHub API versioning
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            //GitHub requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
        });
    }
}