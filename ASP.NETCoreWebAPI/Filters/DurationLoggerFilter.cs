using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NETCoreWebAPI.Filters;

//This is the proper approach to avoid using attribute [DurationLoggerAttribute] on the action
//The functionality is the same, but now it is testable (dependency is obtained easily)
//Used on the UniversityController 
//Filter is registered as Scoped

//In order to apply the filter in the "attribute style" we use:
//[ServiceFilter(typeof(DurationLoggerFilter))]

public class DurationLoggerFilter : IAsyncActionFilter
{
    private readonly ILogger<DurationLoggerFilter> _logger;

    //Then, we can inject the dependency using the constructor. It is fully testable
    public DurationLoggerFilter(ILogger<DurationLoggerFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await next();
        }
        finally
        {
            var text = $"Request completed in {sw.ElapsedMilliseconds}ms";
            _logger.LogInformation(text);
        }
    }
}
