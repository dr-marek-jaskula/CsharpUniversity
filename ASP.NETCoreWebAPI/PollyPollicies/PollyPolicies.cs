using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Caching;
using Polly.Caching.Memory;

namespace ASP.NETCoreWebAPI.PollyPolicies;

public static class PollyPolicies
{
    #region Parameters for policies

    private static readonly ISyncCacheProvider _cacheProvider = new MemoryCacheProvider(new MemoryCache(new MemoryCacheOptions()));
    private static readonly IAsyncCacheProvider _asyncCacheProvider = new MemoryCacheProvider(new MemoryCache(new MemoryCacheOptions()));
    private static readonly ITtlStrategy _ttlRelativeStrategy = new RelativeTtl(TimeSpan.FromMinutes(2));

    private static readonly Action<Context, string> _onCacheGet = (contex, operationalKey) => { Console.WriteLine($"Data get from cache. Cache key: {operationalKey}"); };
    private static readonly Action<Context, string> _onCacheMiss = (contex, operationalKey) => { Console.WriteLine($"Data get from database. Cache key: {operationalKey}"); };
    private static readonly Action<Context, string> _onCachePut = (contex, operationalKey) => { Console.WriteLine($"Data stored in cache. Cache key: {operationalKey}"); };
    private static readonly Action<Context, string, Exception> _onCacheGetError = (contex, operationalKey, exception) => { Console.WriteLine($"Error occurred while getting data from cache! Cache key: {operationalKey}. Exception: {exception.Message}"); };
    private static readonly Action<Context, string, Exception> _onCachePutError = (contex, operationalKey, exception) => { Console.WriteLine($"Error occurred while storing data in cache! Cache key: {operationalKey}. Exception: {exception.Message}"); };

    #endregion Parameters for policies
        
    private static readonly Dictionary<string, Policy> _policies = new();
    private static readonly Dictionary<string, AsyncPolicy> _asyncPolicies = new();

    static PollyPolicies()
    {
        SetPolicies();
    }

    private static void SetPolicies()
    {
        CachePolicy cachePolicy = Policy.Cache(_cacheProvider, _ttlRelativeStrategy, _onCacheGet, _onCacheMiss, _onCachePut, _onCacheGetError, _onCachePutError);
        AsyncCachePolicy asyncCachePolicy = Policy.CacheAsync(_asyncCacheProvider, _ttlRelativeStrategy, _onCacheGet, _onCacheMiss, _onCachePut, _onCacheGetError, _onCachePutError);

        _policies.Add("CacheStrategy", cachePolicy);
        _asyncPolicies.Add("AsyncCacheStrategy", asyncCachePolicy);
    }

    public static Dictionary<string, Policy> GetPolicies()
    {
        return _policies;
    }

    public static Dictionary<string, AsyncPolicy> GetAsyncPolicies()
    {
        return _asyncPolicies;
    }
}
