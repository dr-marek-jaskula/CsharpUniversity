using ASP.NETCoreWebAPI.Models;
using Polly;
using Polly.Registry;

namespace ASP.NETCoreWebAPI.PollyPolicies;

public static class PollyRegistry
{
    //Possible ways to inject the registered Polly policies:
    //1. Add it globally by extension method like below (preferred way)
    //2. Inject the registry by Dependency Injection (by interface: IReadOnlyPolicyRegistry<string> registry)

    // Create a sync policy registry
    public static readonly PolicyRegistry syncRegistry = new();

    // Create a async policy registry
    public static readonly PolicyRegistry asyncRegistry = new();

    public static void ConfigurePollyPolicies(this IServiceCollection services, Dictionary<string, Policy> policies, Dictionary<string, AsyncPolicy> asyncPolicies, Dictionary<string, AsyncPolicy<GitHubUser>> asyncPoliciesGitHubUser, Dictionary<string, AsyncPolicy<HttpResponseMessage>> asyncPoliciesHttpResponseMessage)
    {
        // Populate the registry with policies
        foreach (KeyValuePair<string, Policy> policy in policies)
            syncRegistry.Add(policy.Key, policy.Value);

        foreach (KeyValuePair<string, AsyncPolicy> policy in asyncPolicies)
            asyncRegistry.Add(policy.Key, policy.Value);

        foreach (KeyValuePair<string, AsyncPolicy<GitHubUser>> policy in asyncPoliciesGitHubUser)
            asyncRegistry.Add(policy.Key, policy.Value);

        foreach (KeyValuePair<string, AsyncPolicy<HttpResponseMessage>> policy in asyncPoliciesHttpResponseMessage)
            asyncRegistry.Add(policy.Key, policy.Value);

        // Add Singletons
        services.AddSingleton<IReadOnlyPolicyRegistry<string>>(syncRegistry);
        services.AddSingleton<IReadOnlyPolicyRegistry<string>>(asyncRegistry);
    }
}