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
            await context.Response.WriteAsync(notFoundException.Message);
        }
        catch (BadRequestException badRequestException)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(badRequestException.Message);
        }
        catch (ForbidException forbidException)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync(forbidException.Message);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = 500;

            // Return machine-readable problem details. See RFC 7807 for details.
            // https://datatracker.ietf.org/doc/html/rfc7807#page-6
            var problemDetails = new ProblemDetails //this class represents the standard details
            {
                Type = "https://CsharpUniversityWebApi.com/errors/internal-server-error", //this is just a identifier (can customize)
                Title = "An unrecoverable error occurred",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "This is a demo error used to demonstrate problem details: " + e.Message,
            };
            problemDetails.Extensions.Add("RequestId", context.TraceIdentifier);
            await context.Response.WriteAsJsonAsync(problemDetails, problemDetails.GetType(), null, contentType: "application/problem+json");
        }
    }
}