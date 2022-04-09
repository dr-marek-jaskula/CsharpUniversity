using Polly;
using Polly.Registry;

namespace ASP.NETCoreWebAPI.PollyPolicies;

public static class PollyRegister
{
    public static readonly PolicyRegistry registry = new();

    public static void ConfigurePollyPolicies(this IServiceCollection services, Dictionary<string, Policy> policies, Dictionary<string, AsyncPolicy> asyncPolicies)
    {
        foreach (KeyValuePair<string, Policy> policy in policies)
            registry.Add(policy.Key, policy.Value);
            
        foreach (KeyValuePair<string, AsyncPolicy> policy in asyncPolicies)
            registry.Add(policy.Key, policy.Value);

        services.AddSingleton<IReadOnlyPolicyRegistry<string>>(registry);
    }
}
