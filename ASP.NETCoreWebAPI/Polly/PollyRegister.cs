using ASP.NETCoreWebAPI.Models;
using Polly;
using Polly.Registry;

namespace ASP.NETCoreWebAPI.PollyPolicies;

public static class PollyRegistry
{
    //Possible ways to inject the registered Polly policies:
    //1. Add it globally by extension method like below (preferred way)
    //Reasons why I prefer this way:
    //a) I can have multiple PolicyRegistries and have easy access for them
    //b) Otherwise, I would need to have one registry OR inject IEnumerable<IReadOnlyPolicyRegistry<string>> registries and then get the desired registry

    //2. Inject the registry by Dependency Injection (by interface: IReadOnlyPolicyRegistry<string> registry or IEnumerable<IReadOnlyPolicyRegistry<string>> registries)

    // Create a sync policy registry
    public static readonly PolicyRegistry syncRegistry = new();

    // Create a async policy registry
    public static readonly PolicyRegistry asyncRegistry = new();

    //We can create more PolicyRegistries for a specific (for instance typed) policies like for one containing just policies of type AsyncPolicy<HttpResponseMessage>

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

        // Dependency Injection approach (in such case do not create static PolicyRegistry like above)

        //In this approach, we would need to inject IEnumerable<IReadOnlyPolicyRegistry<string>> registries and select the registry we need by the registry order
        //Of course we could have one Registry and just use the policy name (key) to distinguish the policies. This is also a good option.

        // Add registries
        //services.AddPolicyRegistry(asyncRegistry);
        //services.AddPolicyRegistry(syncRegistry);

        // Or add as Singletons
        //services.AddSingleton<IReadOnlyPolicyRegistry<string>>(syncRegistry);
        //services.AddSingleton<IReadOnlyPolicyRegistry<string>>(asyncRegistry);
    }
}