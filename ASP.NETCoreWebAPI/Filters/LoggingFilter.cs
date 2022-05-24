using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NETCoreWebAPI.Filters;

//Filters are like Middlewares but for the specific endpoint or controller
//In order to apply filters is to use "ServiceFilter" on the certain controller action
//But first we need to register the filter (as a service)
//This is applied to the "Ping" in LogDemoController
public class LoggingFilter : IAsyncActionFilter
{
    private readonly ILogger<LoggingFilter> _logger;

    public LoggingFilter(ILogger<LoggingFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        _logger.LogInformation("Before the endpoint");
        await next();
        _logger.LogInformation("After the endpoint");
    }
}