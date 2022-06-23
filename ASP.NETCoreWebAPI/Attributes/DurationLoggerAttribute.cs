using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ResolvingDeps.WebApi.Attributes;

//This attribute is used in the UniversityController
//This is bad approach, because testing it is very hard! Use is only, ONLY is super rare, specific cases. 
//In MOST cases use the Filter approach to achieve the same functionality (Go to: DurationLoggerFilter)

//This is an attributes demonstrating how to inject the dependencies to it.
//Due to the fact that we can only have constant parameters, we can not inject the dependency
//Therefore, to inject the service, we would use the HttpContext to get the reference to the ServiceProvider 
//From the instance of the ServiceProvider we can get any dependency we would like to get (use GetRequiredService rather then GetService)

public class DurationLoggerAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await next();
        }
        finally
        {
            //This is bad approach, because testing it is very hard! Use is only, ONLY is super rare, specific cases. 
            //In MOST cases use the Filter approach to achieve the same functionality (Go to: DurationLoggerFilter)

            //Getting serviceProvider from the context to be able to inject the dependency
            var serviceProvider = context.HttpContext.RequestServices;
            //inject the dependency 
            var logger = serviceProvider.GetRequiredService<ILogger<DurationLoggerAttribute>>();

            var text = $"Request completed in {sw.ElapsedMilliseconds}ms";
            logger.LogInformation(text);
            //Console.WriteLine(text);
        }
    }
}
