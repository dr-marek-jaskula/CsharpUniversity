using ASP.NETCoreWebAPI.Exceptions;
using ASP.NETCoreWebAPI.Models;
using ASP.NETCoreWebAPI.PollyPolicies;
using Newtonsoft.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;
using Polly.Wrap;

namespace ASP.NETCoreWebAPI.Services;

public interface IGitHubService
{
    Task<GitHubUser?> GetUserByUserNameAsyncPollyRetry(string userName);

    Task<GitHubUser?> GetUserByUserNameAsyncPollyCB(string userName);

    Task<GitHubUser?> GetUserByUserNameAsyncPollyAdvanceCB(string userName);

    Task<GitHubUser?> GetUserByUserNameAsyncTimeoutPolly(string userName);

    Task<GitHubUser?> GetUserByUserNameAsyncTimeoutPolly2(string userName);

    Task<GitHubUser?> GetUserByUserNameAsyncTimeoutPolly3(string userName, CancellationToken token);

    Task<GitHubUser?> GetUserByUserNameAsyncTimeoutPolly4(string userName);

    Task<GitHubUser?> GetUserByUserNameAsyncCachePolly(string userName);

    Task<GitHubUser?> GetUserByUserNameAsyncCachePolly2(string userName);

    Task<GitHubUser?> GetUserByUserNameAsyncCachePolly3(string userName);

    Task<GitHubUser?> GetUserByUserNameAsyncFallbackPolly(string userName);

    Task<GitHubUser?> GetUserByUserNameAsyncFallbackPolly2(string userName, CancellationToken token);

    Task<GitHubUser?> GetUserByUserNameAsyncWrapMultiplePolicies(string userName, CancellationToken token);
}

public class GitHubService : IGitHubService
{
    //Best way to create a HttpCient. At first Clients need to be configured (Headers etc.) in Program.cs (after Polly registration)
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly Random _random = new();

    public GitHubService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    #region Retry

    public async Task<GitHubUser?> GetUserByUserNameAsyncPollyRetry(string userName)
    {
        var client = _httpClientFactory.CreateClient("GitHub");

        AsyncPolicy pollyPolicy = (AsyncPolicy)PollyRegister.asyncRegistry["RetryStrategy"];

        return await pollyPolicy.ExecuteAsync(async () =>
        {
            if (_random.Next(1, 3) == 1)
                throw new HttpRequestException("This is a fake request exception");

            var result = await client.GetAsync($"/users/{userName}");

            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var resultString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GitHubUser>(resultString);
        });
    }

    #endregion Retry

    #region CircuitBreaker

    public async Task<GitHubUser?> GetUserByUserNameAsyncPollyCB(string userName)
    {
        AsyncCircuitBreakerPolicy pollyCircuitBreakerPolicy = (AsyncCircuitBreakerPolicy)PollyRegister.asyncRegistry["CircuitBreakerStrategy"];

        Console.WriteLine($"Circuit state {pollyCircuitBreakerPolicy.CircuitState}");

        if (pollyCircuitBreakerPolicy.CircuitState is CircuitState.Open)
            throw new UnavailableException("Service is currently unavailable");

        var client = _httpClientFactory.CreateClient("GitHub");

        AsyncPolicy pollyRetryPolicy = (AsyncPolicy)PollyRegister.asyncRegistry["RetryStrategy2"];

        var task = pollyRetryPolicy.ExecuteAsync(async () =>
        {
            //if (_random.Next(1, 3) == 1)
            throw new HttpRequestException("This is a fake request exceptionv");

            var result = await client.GetAsync($"/users/{userName}");

            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var resultString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GitHubUser>(resultString);
        });

        try
        {
            var response = await pollyCircuitBreakerPolicy.ExecuteAsync(() => task);
            return response;
        }
        catch
        {
            Console.WriteLine($"Circuit state {pollyCircuitBreakerPolicy.CircuitState}");
            throw;
        }
    }

    public async Task<GitHubUser?> GetUserByUserNameAsyncPollyAdvanceCB(string userName)
    {
        AsyncCircuitBreakerPolicy pollyCircuitBreakerPolicy = (AsyncCircuitBreakerPolicy)PollyRegister.asyncRegistry["CircuitBreakerStrategy2"];

        Console.WriteLine($"Circuit state {pollyCircuitBreakerPolicy.CircuitState}");

        if (pollyCircuitBreakerPolicy.CircuitState is CircuitState.Open)
            throw new UnavailableException("Service is currently unavailable");

        var client = _httpClientFactory.CreateClient("GitHub");

        AsyncPolicy pollyRetryPolicy = (AsyncPolicy)PollyRegister.asyncRegistry["RetryStrategy2"];

        var task = pollyRetryPolicy.ExecuteAsync(async () =>
        {
            if (_random.Next(1, 3) == 1)
                throw new HttpRequestException("This is a fake request exception");

            var result = await client.GetAsync($"/users/{userName}");

            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var resultString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GitHubUser>(resultString);
        });

        try
        {
            var response = await pollyCircuitBreakerPolicy.ExecuteAsync(() => task);
            return response;
        }
        catch
        {
            Console.WriteLine($"Circuit state {pollyCircuitBreakerPolicy.CircuitState}");
            throw;
        }
    }

    #endregion CircuitBreaker

    #region Timeouts

    //Pessimistic version (without CancellationToken)
    public async Task<GitHubUser?> GetUserByUserNameAsyncTimeoutPolly(string userName)
    {
        AsyncPolicy pollyPolicy = (AsyncPolicy)PollyRegister.asyncRegistry["TimeoutStrategy2"];

        var client = _httpClientFactory.CreateClient("GitHub");

        return await pollyPolicy.ExecuteAsync(async () =>
        {
            await Task.Delay(3000);

            var result = await client.GetAsync($"/users/{userName}");
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var resultString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GitHubUser>(resultString);
        });
    }

    //Optimistic version (with token similar to CancellationToken.None)
    public async Task<GitHubUser?> GetUserByUserNameAsyncTimeoutPolly2(string userName)
    {
        var client = _httpClientFactory.CreateClient("GitHub");

        AsyncPolicy pollyPolicy = (AsyncPolicy)PollyRegister.asyncRegistry["TimeoutStrategy3"];

        //CancellationTokenSource which will create our Token. It is important to dispose the tokenSource after it is used.
        CancellationTokenSource tokenSource = new();
        //We create token, but in a raw version (similar to CancellationToken.None)
        CancellationToken cancellationToken = tokenSource.Token;

        return await pollyPolicy.ExecuteAsync(async cancellationToken =>
        {
            Console.WriteLine(cancellationToken.IsCancellationRequested);
            Console.WriteLine("I'm before the 3 seconds delay");
            await Task.Delay(3000);
            Console.WriteLine("I'm after the 3 second delay");
            Console.WriteLine(cancellationToken.IsCancellationRequested);

            HttpResponseMessage result = await client
                //GetAsync will be canceled when either the timeout occurs, or userCancellationSource is signaled.
                .GetAsync($"/users/{userName}", cancellationToken)
                //It is important to dispose the tokenSource after it is used.
                .ContinueWith((task) => { tokenSource.Dispose(); return task.Result; });

            //if searching takes too much time, the cancellation will be done here.
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            Console.WriteLine(cancellationToken.IsCancellationRequested);
            Console.WriteLine("I'm after connecting to the GitHub");

            var resultString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GitHubUser>(resultString);
        }, cancellationToken: cancellationToken);
        //Implement task with cancellationToken set to none, with is better then go with Pessimistic case. However the CancellationToken can be designed.
        //This scenario the Cancellation Token is set to handle the timeout 1,5s (so basically its not None - to None was added the timeout predicate)
    }

    //Optimistic version (with CancellationToken given from controller level)
    //Token is sent from the controller (from client that connects to our API). The client must have cancel ability (like "cancel" button in postman)
    public async Task<GitHubUser?> GetUserByUserNameAsyncTimeoutPolly3(string userName, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient("GitHub");

        AsyncPolicy pollyPolicy = (AsyncPolicy)PollyRegister.asyncRegistry["TimeoutStrategy3"];

        return await pollyPolicy.ExecuteAsync(async cancellationToken =>
        {
            Console.WriteLine(cancellationToken.IsCancellationRequested);
            Console.WriteLine("I'm before the 3 seconds delay");
            await Task.Delay(3000, cancellationToken).ContinueWith(task =>
            {
                Console.WriteLine("Task was canceled");
                cancellationToken.ThrowIfCancellationRequested();
            });
            Console.WriteLine("I'm after the 3 second delay");
            Console.WriteLine(cancellationToken.IsCancellationRequested);

            HttpResponseMessage? result = await client.GetAsync($"/users/{userName}", cancellationToken).ContinueWith(task =>
            {
                if (task.Status is not (TaskStatus.Canceled or TaskStatus.Faulted))
                    return task.Result;
                else if (task.Status is TaskStatus.Canceled)
                    cancellationToken.ThrowIfCancellationRequested();

                return null;
            }, cancellationToken); //GetAsync will be canceled when either the timeout occurs, or userCancellationSource is signaled.

            //if searching takes too much time, the cancellation will be done here.
            if (result is null || result.StatusCode is System.Net.HttpStatusCode.NotFound)
                return null;

            Console.WriteLine(cancellationToken.IsCancellationRequested);
            Console.WriteLine("I'm after connecting to the GitHub");

            var resultString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GitHubUser>(resultString);
        }, cancellationToken: cancellationToken);
    }

    //More Timeouts with Polly with made CancellationToken (Cancellation Token should be provided by the user)
    public async Task<GitHubUser?> GetUserByUserNameAsyncTimeoutPolly4(string userName)
    {
        var client = _httpClientFactory.CreateClient("GitHub");

        AsyncPolicy pollyPolicy = (AsyncPolicy)PollyRegister.asyncRegistry["TimeoutStrategy3"];

        //CancellationTokenSource which will create our Token. It is important to dispose the tokenSource after it is used.
        CancellationTokenSource tokenSource = new();
        //We create token, but in a raw version (similar to CancellationToken.None)
        CancellationToken cancellationToken = tokenSource.Token;

        return await pollyPolicy.ExecuteAsync(async cancellationToken =>
        {
            Console.WriteLine(cancellationToken.IsCancellationRequested);

            await Task.Delay(1000);

            Console.WriteLine(cancellationToken.IsCancellationRequested);

            //this is a proper way to check if the cancellation was requested and to throw the exception in a safe way. The exception is "OperationCanceledException"
            cancellationToken.ThrowIfCancellationRequested();

            await Task.Delay(1000);

            Console.WriteLine(cancellationToken.IsCancellationRequested);  //its ready to be canceled. Now need to ThrowIfCancelationRequested or use Cancel() on tokenSource

            //this is a proper way to check if the cancellation was requested and to throw the exception in a safe way. The exception is "OperationCanceledException"
            if (cancellationToken.IsCancellationRequested)
            {
                tokenSource.Dispose(); //a way to dispose the token source (it is important to free the resources) if its not in a task, can also use "using" statement
                cancellationToken.ThrowIfCancellationRequested();
            }

            var result = await client.GetAsync($"/users/{userName}", cancellationToken: cancellationToken);
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var resultString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GitHubUser>(resultString);
        }, cancellationToken: cancellationToken);
    }

    #endregion Timeouts

    #region Cache

    // Basic version with relative time-to-live strategy (ttl).
    // After caching will always give the same result because operational Key is always the same.
    // A good example how caching works and what is the cache key responsibility (do not use in real app)
    public async Task<GitHubUser?> GetUserByUserNameAsyncCachePolly(string userName)
    {
        var client = _httpClientFactory.CreateClient("GitHub");

        AsyncPolicy pollyPolicy = (AsyncPolicy)PollyRegister.asyncRegistry["CacheStrategy"];

        return await pollyPolicy.ExecuteAsync(async context =>
        {
            var result = await client.GetAsync($"/users/{userName}");

            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var resultString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GitHubUser>(resultString);
        }, new Context("MyOperationalKey"));
        //The above pattern assumes you are using the default CacheKeyStrategy. (check it in policy definition)
    }

    // Basic version with relative time-to-live strategy (ttl).
    // Cache OperationalKey is now mapped to the userName. Therefore caching is mapped to the certain user
    public async Task<GitHubUser?> GetUserByUserNameAsyncCachePolly2(string userName)
    {
        var client = _httpClientFactory.CreateClient("GitHub");

        AsyncPolicy pollyPolicy = (AsyncPolicy)PollyRegister.asyncRegistry["CacheStrategy"];

        return await pollyPolicy.ExecuteAsync(async context =>
        {
            var result = await client.GetAsync($"/users/{userName}");

            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var resultString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GitHubUser>(resultString);
        }, new Context(userName));
    }

    // Basic version with relative time-to-live strategy (ttl)
    public async Task<GitHubUser?> GetUserByUserNameAsyncCachePolly3(string userName)
    {
        var client = _httpClientFactory.CreateClient("GitHub");

        AsyncPolicy pollyPolicy = (AsyncPolicy)PollyRegister.asyncRegistry["CacheStrategy2"];

        //pollyPolicy with ttlSlidingStrategy
        return await pollyPolicy.ExecuteAsync(async context =>
        {
            await Task.Delay(3000); //good way to show the info is cached

            var result = await client.GetAsync($"/users/{userName}");

            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var resultString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GitHubUser>(resultString);
        }, new Context($"OperationalKey for: {userName}")); //Cache OperationalKey is now mapped to the userName.
    }

    //For custom operational key strategy examine CacheStrategy3

    #endregion Cache

    #region Fallback

    public async Task<GitHubUser?> GetUserByUserNameAsyncFallbackPolly(string userName)
    {
        var client = _httpClientFactory.CreateClient("GitHub");

        AsyncFallbackPolicy<GitHubUser> pollyPolicy = (AsyncFallbackPolicy<GitHubUser>)PollyRegister.asyncRegistry["UserFallbackStrategy"];

        return await pollyPolicy.ExecuteAsync(async () =>
        {
            throw new TaskCanceledException(); //so this exception will result in giving the new GitHub user by fallback policy

            var result = await client.GetAsync($"/users/{userName}");

            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var resultString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GitHubUser>(resultString);
        });
    }

    //Add CancelationToken
    public async Task<GitHubUser?> GetUserByUserNameAsyncFallbackPolly2(string userName, CancellationToken token)
    {
        var client = _httpClientFactory.CreateClient("GitHub");

        AsyncFallbackPolicy<GitHubUser> pollyPolicy = (AsyncFallbackPolicy<GitHubUser>)PollyRegister.asyncRegistry["UserFallbackStrategy2"];

        return await pollyPolicy.ExecuteAsync(async (cancellationToken) =>
        {
            //???????? Why cancellation token does not execute strategy message
            //await Task.Delay(3000, cancellationToken).ContinueWith(task =>
            //{
            //    Console.WriteLine("Task was canceled");
            //    cancellationToken.ThrowIfCancellationRequested();
            //});

            throw new TaskCanceledException(); //so this exception will result in giving the new GitHub user by fallback policy

            var result = await client.GetAsync($"/users/{userName}");

            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var resultString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GitHubUser>(resultString);
        }, cancellationToken: token);
    }

    #endregion Fallback

    #region Wrap multiple Policies

    public async Task<GitHubUser?> GetUserByUserNameAsyncWrapMultiplePolicies(string userName, CancellationToken cancellationToken)
    {
        AsyncCircuitBreakerPolicy pollyCircuitBreakerPolicy = (AsyncCircuitBreakerPolicy)PollyRegister.asyncRegistry["CircuitBreakerStrategy2"];

        Console.WriteLine($"Circuit state {pollyCircuitBreakerPolicy.CircuitState}");

        if (pollyCircuitBreakerPolicy.CircuitState is CircuitState.Open)
            throw new UnavailableException("Service is currently unavailable"); //To verify if CircuitBreaker is working

        var client = _httpClientFactory.CreateClient("GitHub");

        AsyncPolicyWrap<GitHubUser> pollyMultiplePolicies = (AsyncPolicyWrap<GitHubUser>)PollyRegister.asyncRegistry["UserUltimateStrategy"];

        return await pollyMultiplePolicies.ExecuteAsync(async (context, token) =>
        {
            //await Task.Delay(10000);
            //Console.WriteLine(token.IsCancellationRequested);
            //token.ThrowIfCancellationRequested(); //another way to examine the timeout

            //await Task.Delay(3000); //cache is working
            //throw new TaskCanceledException(); //fallback is working

            //await Task.Delay(3000, token); //timeout is working

            if (_random.Next(1, 3) == 1)
                throw new HttpRequestException("This is a fake request exception"); //retry is working //Circuit-breaker is working: RETRIES do not work on CIRCUIT-BREAKER

            var result = await client.GetAsync($"/users/{userName}", token);

            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            var resultString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GitHubUser>(resultString);
        }, context: new Context($"OperationalKey for: {userName}"), cancellationToken: cancellationToken);
    }

    #endregion Wrap multiple Policies
}