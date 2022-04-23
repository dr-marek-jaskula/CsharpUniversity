using ASP.NETCoreWebAPI.Models;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Caching;
using Polly.Caching.Memory;
using Polly.CircuitBreaker;
using Polly.Fallback;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;

namespace ASP.NETCoreWebAPI.PollyPolicies;

public static class PollyPolicies
{
    #region Parameters for Policies

    private const int _maxRetries = 3;
    private static readonly Random _random = new();

    private static readonly Action<Exception, TimeSpan> _onBreak = (Exception, timeSpan) => { Console.WriteLine("I'm on break"); };
    private static Action _onReset = () => { Console.WriteLine("I'm on reset"); };

    private static readonly ISyncCacheProvider _cacheProvider = new MemoryCacheProvider(new MemoryCache(new MemoryCacheOptions()));
    private static readonly IAsyncCacheProvider _asyncCacheProvider = new MemoryCacheProvider(new MemoryCache(new MemoryCacheOptions()));
    private static readonly ITtlStrategy _ttlRelativeStrategy = new RelativeTtl(TimeSpan.FromMinutes(2));
    private static readonly ITtlStrategy _ttlAbsoluteStrategy = new AbsoluteTtl(DateTimeOffset.Now.Date.AddDays(1)); //to the midnight
    private static readonly ITtlStrategy _ttlSlidingStrategy = new SlidingTtl(TimeSpan.FromMinutes(1)); //item remains valid for next 2min, every time cache is used

    //Basic operation key strategy. This is equivalent to "DefaultCacheKeyStrategy.Instance.GetCacheKey"
    private static readonly Func<Context, string> _cacheKeyStrategy = context => context.OperationKey;

    private static Guid _guid = Guid.NewGuid();

    //a custom keyStrategy, adding Guid to the operation Key
    private static readonly Func<Context, string> _customCacheKeyStrategy = context => context.OperationKey + context["_guid"];

    private static readonly Action<Context, string> _onCacheGet = (contex, operationalKey) => { Console.WriteLine($"Data get from cache. Cache key: {operationalKey}"); };
    private static readonly Action<Context, string> _onCacheMiss = (contex, operationalKey) => { Console.WriteLine($"Data get from database. Cache key: {operationalKey}"); };
    private static readonly Action<Context, string> _onCachePut = (contex, operationalKey) => { Console.WriteLine($"Data stored in cache. Cache key: {operationalKey}"); };
    private static readonly Action<Context, string, Exception> _onCacheGetError = (contex, operationalKey, exception) => { Console.WriteLine($"Error occurred while getting data from cache! Cache key: {operationalKey}. Exception: {exception.Message}"); };
    private static readonly Action<Context, string, Exception> _onCachePutError = (contex, operationalKey, exception) => { Console.WriteLine($"Error occurred while storing data in cache! Cache key: {operationalKey}. Exception: {exception.Message}"); };

    #endregion Parameters for Policies

    #region Policy Dictionaries

    public static readonly Dictionary<string, Policy> syncPolicies = new();
    public static readonly Dictionary<string, AsyncPolicy> asyncPolicies = new();

    //custom policies with TResult. One dictionary for one TResult. For each dictionary need to add foreach loop into the class PollyRegister.
    public static readonly Dictionary<string, AsyncPolicy<GitHubUser>> asyncPoliciesGitHubUser = new();

    public static readonly Dictionary<string, AsyncPolicy<HttpResponseMessage>> asyncPoliciesHttpResponseMessage = new();

    #endregion Policy Dictionaries

    /*
        policyResult.Outcome - whether the call succeeded or failed
        policyResult.FinalException - the final exception captured, will be null if the call succeeded
        policyResult.ExceptionType - was the final exception an exception the policy was defined to handle (like HttpRequestException above) or an unhandled one (Exception). Will be null if the call succeeded.
        policyResult.Result - if executing a func, the result if the call succeeded or the type's default value
    */

    static PollyPolicies()
    {
        SetSyncPolicies();
        SetAsyncPolicies();
    }

    #region Policy Setters

    private static void SetSyncPolicies()
    {
        //Create policies:

        Policy syncRetryPolicyWithKeys = Policy.Handle<HttpRequestException>().Retry(3, onRetry: (exception, retryCount, context) =>
        {
            Console.WriteLine($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
        }).WithPolicyKey("MyDataAccessPolicy");

        // in order to set the PolicyKey, policy must by defined by "Policy" class.
        // Identify call sites with an OperationKey, by passing in a Context
        // in order to do this and give the Context a key:
        // policyWithKeys.Execute(context => { Console.WriteLine($"Hello {context}"); }, new Context("GetCustomerDetails"));

        // "MyDataAccessPolicy" -> context.PolicyKey
        // "GetCustomerDetails  -> context.OperationKey

        //Retry
        RetryPolicy syncRetryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetry(_maxRetries, times => TimeSpan.FromMilliseconds(times * 1000 + _random.Next(0, 1000)), onRetry: (exception, timeSpan) => { Console.WriteLine($"I'm retrying. Exception was: {exception} and time span {timeSpan.TotalSeconds} total seconds"); });

        //Cache
        CachePolicy syncCachePolicy = Policy.Cache(_cacheProvider, _ttlRelativeStrategy, _onCacheGet, _onCacheMiss, _onCachePut, _onCacheGetError, _onCachePutError);

        //Create TResult policies:

        //Add non-generic policies to the sync dictionary
        syncPolicies.Add("RepositoryResilienceStrategy", syncRetryPolicy);
        syncPolicies.Add("CacheStrategy", syncCachePolicy);
        syncPolicies.Add("RetryWithKeys", syncRetryPolicyWithKeys);

        //Add generic policies to the dictionary (one dictionary for one specific generic type of policy)
    }

    private static void SetAsyncPolicies()
    {
        //Create policies:

        #region Retry

        //It is good to have randomized retry, because u can ddos your app by have multiple same time requests
        AsyncRetryPolicy asyncRetryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetryAsync(_maxRetries, times => TimeSpan.FromMilliseconds(times * 1000 + _random.Next(0, 1000)), onRetry: (exception, timeSpan) => { Console.WriteLine($"I'm retrying. Exception message is: {exception.Message} and time span {timeSpan.TotalSeconds} total seconds"); });
        // Retry policy with additional predicate.
        AsyncRetryPolicy asyncRetryPolicy2 = Policy.Handle<HttpRequestException>(exception => exception.Message is not "This is a fake request exception").WaitAndRetryAsync(_maxRetries, times => TimeSpan.FromMilliseconds(times * 1000 + _random.Next(0, 1000)));

        #endregion Retry

        #region Circuit-breaker

        //Circuit-breaker
        //Standard policy: this policy will result in opening the circuit after two fails, the duration of the circuit is 1minute, then if goes to half-open state - where the first new request will determine if the circuit should be opened or closed again.
        AsyncCircuitBreakerPolicy asyncCircuitBreakerPolicy = Policy.Handle<HttpRequestException>().CircuitBreakerAsync(2, TimeSpan.FromMinutes(1), _onBreak, _onReset);

        //Advanced policy where failureThreshold is % of fail requests, sampligDuration is time period to examine failures, minimumThroughput is a number of requests that must be to examine circuit, durationOfBreak is standard duration of
        AsyncCircuitBreakerPolicy asyncCircuitBreakerPolicy2 = Policy.Handle<HttpRequestException>().AdvancedCircuitBreakerAsync(0.5, TimeSpan.FromSeconds(5), 5, TimeSpan.FromSeconds(30));

        #endregion Circuit-breaker

        #region Cache

        AsyncCachePolicy asyncCachePolicy = Policy.CacheAsync(_asyncCacheProvider, _ttlRelativeStrategy, _onCacheGet, _onCacheMiss, _onCachePut, _onCacheGetError, _onCachePutError);
        AsyncCachePolicy asyncCachePolicy2 = Policy.CacheAsync(_asyncCacheProvider, _ttlSlidingStrategy, DefaultCacheKeyStrategy.Instance.GetCacheKey, _onCacheGet, _onCacheMiss, _onCachePut, _onCacheGetError, _onCachePutError);
        AsyncCachePolicy asyncCachePolicy3 = Policy.CacheAsync(_asyncCacheProvider, _ttlSlidingStrategy, _customCacheKeyStrategy, _onCacheGet, _onCacheMiss, _onCachePut, _onCacheGetError, _onCachePutError);

        #endregion Cache

        #region Timeout

        //Advanced policy with a lambda expression executing after timeout. Lambda must return a task
        AsyncTimeoutPolicy asyncTimeoutPolicy = Policy.TimeoutAsync(TimeSpan.FromMilliseconds(1500), onTimeoutAsync: (context, timespan, task) =>
        {
            //tokenSource.Dispose(); //remember to dispose the tokenSource if it was created and it is reachable. Can be disposed in other place, not here, but can be here if its reachable
            return Task.Run(() => { Console.WriteLine($"Timeout! {context.PolicyKey} at {context.OperationKey}: execution timed out after {timespan.TotalSeconds} seconds."); });
        });

        //TimeoutStrategy.Pessimistic - action does not require CancellationToken.
        AsyncTimeoutPolicy asyncTimeoutPolicy2 = Policy.TimeoutAsync(TimeSpan.FromMilliseconds(1500), TimeoutStrategy.Pessimistic);

        //TimeoutStrategy.Optimistic - require the CancellationToken. This token is used to cancel the task while it is in progress. It can be set as none as below in method or like this:
        AsyncTimeoutPolicy asyncTimeoutPolicy3 = Policy.TimeoutAsync(TimeSpan.FromMilliseconds(1500), TimeoutStrategy.Optimistic);

        #endregion Timeout

        #region Fallback

        //For instance we can use them for PUT requests - inserting data into db
        AsyncFallbackPolicy asyncFallbackPolicy = Policy.Handle<TaskCanceledException>().FallbackAsync(token => Task.Run(() => { Console.WriteLine("Its a Fallback"); }));
        AsyncFallbackPolicy asyncFallbackPolicy2 = Policy.Handle<TaskCanceledException>().FallbackAsync(token => Task.Run(() => { Console.WriteLine("Its a Fallback"); }), onFallbackAsync: exception =>
        {
            return Task.Run(
                () => { Console.WriteLine($"Message: {exception.Message}. InnerException: {exception.InnerException}."); }
            );
        });

        #endregion Fallback

        //Create TResult policies:

        //CircuitBreaker
        //This policy include additional predicate (condition), and aim for the HttpResponseMessage result
        AsyncCircuitBreakerPolicy<HttpResponseMessage> asyncResponseCircuitBreakerPolicy = Policy.HandleResult<HttpResponseMessage>(message => (int)message.StatusCode == 503).CircuitBreakerAsync(2, TimeSpan.FromMinutes(1));

        //Fallback
        //For instance we can use them for GET requests - getting data from db
        AsyncFallbackPolicy<GitHubUser> asyncUserFallbackPolicy = Policy<GitHubUser>.Handle<TaskCanceledException>().FallbackAsync(new GitHubUser());
        AsyncFallbackPolicy<GitHubUser> asyncUserFallbackPolicy2 = Policy<GitHubUser>.Handle<TaskCanceledException>().Or<OperationCanceledException>().FallbackAsync(token => Task.Run(() =>
        {
            Console.WriteLine("Fallback is on");
            return new GitHubUser();
        }));
        AsyncFallbackPolicy<GitHubUser> asyncUserFallbackPolicy3 = Policy<GitHubUser>.Handle<TaskCanceledException>().FallbackAsync(token => Task.Run(() => { Console.WriteLine("Fallback is on"); return new GitHubUser(); }), onFallbackAsync: result =>
        {
            return Task.Run(
                () => { Console.WriteLine($"Exception message: {result.Exception.Message}. Result: {result.Result}."); }
            );
        });

        //Wrap many policies into one grouped policy
        //The generic policy wrap that most inner is timeout, then breaker, retry, cache and the most outer is fallback
        AsyncPolicyWrap<GitHubUser> multiplePolicies = asyncUserFallbackPolicy.WrapAsync(asyncCachePolicy).WrapAsync(asyncRetryPolicy).WrapAsync(asyncCircuitBreakerPolicy).WrapAsync(asyncTimeoutPolicy);

        //Add non-generic policies to the dictionary
        asyncPolicies.Add("RetryStrategy", asyncRetryPolicy);
        asyncPolicies.Add("RetryStrategy2", asyncRetryPolicy2);
        asyncPolicies.Add("CircuitBreakerStrategy", asyncCircuitBreakerPolicy);
        asyncPolicies.Add("CircuitBreakerStrategy2", asyncCircuitBreakerPolicy2);
        asyncPolicies.Add("AsyncCacheStrategy", asyncCachePolicy);
        asyncPolicies.Add("CacheStrategy", asyncCachePolicy);
        asyncPolicies.Add("CacheStrategy2", asyncCachePolicy2);
        asyncPolicies.Add("CacheStrategy3", asyncCachePolicy3);
        asyncPolicies.Add("TimeoutStrategy", asyncTimeoutPolicy);
        asyncPolicies.Add("TimeoutStrategy2", asyncTimeoutPolicy2);
        asyncPolicies.Add("TimeoutStrategy3", asyncTimeoutPolicy3);
        asyncPolicies.Add("FallbackStrategy", asyncFallbackPolicy);
        asyncPolicies.Add("FallbackStrategy1", asyncFallbackPolicy2);

        //Add generic policies to the dictionary (one dictionary for one specific generic type of policy)
        asyncPoliciesHttpResponseMessage.Add("CircuitBreakerStrategy3", asyncResponseCircuitBreakerPolicy);

        asyncPoliciesGitHubUser.Add("UserFallbackStrategy", asyncUserFallbackPolicy);
        asyncPoliciesGitHubUser.Add("UserFallbackStrategy2", asyncUserFallbackPolicy2);
        asyncPoliciesGitHubUser.Add("UserFallbackStrategy3", asyncUserFallbackPolicy3);

        asyncPoliciesGitHubUser.Add("UserUltimateStrategy", multiplePolicies);
    }

    #endregion Policy Setters

    #region Policy Getters

    public static Dictionary<string, Policy> GetSyncPolicies()
    {
        return syncPolicies;
    }

    public static Dictionary<string, AsyncPolicy> GetAsyncPolicies()
    {
        return asyncPolicies;
    }

    public static Dictionary<string, AsyncPolicy<GitHubUser>> GetAsyncPoliciesGitHubUser()
    {
        return asyncPoliciesGitHubUser;
    }

    public static Dictionary<string, AsyncPolicy<HttpResponseMessage>> GetAsyncPoliciesHttpResponseMessage()
    {
        return asyncPoliciesHttpResponseMessage;
    }

    #endregion Policy Getters
}