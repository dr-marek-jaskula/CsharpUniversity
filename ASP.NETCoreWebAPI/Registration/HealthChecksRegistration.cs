using ASP.NETCoreWebAPI.HealthChecks;
using LanguageExt;

namespace Microsoft.Extensions.DependencyInjection;

public static class HealthChecksRegistration
{
    public static void RegisterHealthChecks(this IServiceCollection services, string connectionString)
    {
        //We need to also Map to the endpoint in the "Configure HTTP request pipeline" region, using the minimal API approach (see app.UseEndpoints in program.cs)
        //Add SqlServer health checks and custom one MyHealthCheck(go to: HealthChecks folder)

        services.AddHealthChecks()
            .AddSqlServer(connectionString)
            .AddCheck<RandomHealthCheck>("Random health check")
            .AddCheck<EndpointHealthCheck>("Endpoint health check");
        
        services.AddHealthChecksUI().AddInMemoryStorage();
    }
}
