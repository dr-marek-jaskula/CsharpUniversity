using System.Diagnostics;

namespace ASP.NETCoreWebAPI.Middlewares;

/// <summary>
/// Examines if the request takes more then 4 second. If yes, then log the request
/// </summary>
public class RequestTimeMiddleware : IMiddleware
{
    private readonly ILogger<RequestTimeMiddleware> _logger;
    private Stopwatch _stopwatch;

    public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
    {
        _stopwatch = new Stopwatch();
        _logger = logger;
    }

    //The RequestDelegate can be also injected by constructor 
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _stopwatch.Start();
        await next.Invoke(context);
        _stopwatch.Stop();

        var elapsedMiliseconds = _stopwatch.ElapsedMilliseconds;
        if (elapsedMiliseconds / 1000 > 4)
        {
            var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {elapsedMiliseconds} ms";
            _logger.LogInformation(message);
        }
    }
}
