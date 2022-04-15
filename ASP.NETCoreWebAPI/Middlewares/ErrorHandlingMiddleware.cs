using ASP.NETCoreWebAPI.Exceptions;

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
        catch (Exception)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}