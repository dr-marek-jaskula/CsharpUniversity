using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace ASP.NETCoreWebAPI.HealthChecks;

public class EndpointHealthCheck : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var catUrl = "https://localhost:7240/api/University/async?api-version=1.0";

        var client = new HttpClient
        {
            BaseAddress = new Uri(catUrl)
        };

        HttpResponseMessage response = await client.GetAsync("");

        return response.StatusCode == HttpStatusCode.OK ?
            await Task.FromResult(new HealthCheckResult(
                  status: HealthStatus.Healthy,
                  description: "API is healthy")) :
            await Task.FromResult(new HealthCheckResult(
                  status: HealthStatus.Unhealthy,
                  description: "API is sick"));
    }
}