using ASP.NETCoreWebAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPI.Middlewares;

/// <summary>
/// Middleware that provides exception handling. Each request needs to be processed by the following try-catch block
/// </summary>
public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFoundException)
        {
            context.Response.StatusCode = 404;
            await HandleExceptionAsync(context, notFoundException);
        }
        catch (BadRequestException badRequestException)
        {
            context.Response.StatusCode = 400;
            await HandleExceptionAsync(context, badRequestException);
        }
        catch (ForbidException forbidException)
        {
            context.Response.StatusCode = 403;
            await HandleExceptionAsync(context, forbidException);
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = 500;
            await HandleExceptionAsync(context, exception);
        }
    }

    /// <summary>
    /// Handler that sends the enough information to user so that he would be able to identify the problem and explain the details to developer.
    /// However, the detail does not cover the whole problem, in order to prevent the user from getting the sensitive data or unreadable for user data.
    /// </summary>
    /// <param name="context">HttpContext</param>
    /// <param name="exception">Request exception</param>
    /// <returns>Exception details in JSON format</returns>
    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Return machine-readable problem details. See RFC 7807 for details.
        // https://datatracker.ietf.org/doc/html/rfc7807#page-6
        var problemDetails = new ProblemDetails //this class represents the standard details. Very important class in real-life web api
        {
            Type = "https://CsharpUniversityWebApi.com/errors/internal-server-error", //this is URI (uniform resource identifier) [not URL!!] just a identifier (can customize for example for database problem or division by 0 problems)
            Title = "An unrecoverable error occurred",
            Status = context.Response.StatusCode,
            Detail = "This is a demo error used to demonstrate problem details: " + exception.Message,
            Instance = context.Request.Path
        };
        //Then add additional data that is important for us like "RequestId". This RequestId can be then use by seq filtering. This is one of the major points of ProblemDetails
        problemDetails.Extensions.Add("RequestId", context.TraceIdentifier);
        await context.Response.WriteAsJsonAsync(problemDetails, problemDetails.GetType(), options: null, contentType: "application/problem+json");
    }
}