using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ASP.NETCoreWebAPI.HealthChecks;

//This class need to be registered in the healthchecks, in the configuration region of program.cs
public class RandomHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        int result = new Random().Next(1, 4);
        //randomize the health of the application for tutorial purposes
        if (result == 1)
            return Task.FromResult(HealthCheckResult.Healthy(description: "Success"));

        if (result == 2)
            return Task.FromResult(HealthCheckResult.Degraded(description: "Success"));

        return Task.FromResult(HealthCheckResult.Unhealthy(description: "Failed"));
    }
}