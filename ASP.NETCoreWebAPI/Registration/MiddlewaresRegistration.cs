using ASP.NETCoreWebAPI.Middlewares;

namespace Microsoft.Extensions.DependencyInjection;

public static class MiddlewaresRegistration
{
    public static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<ErrorHandlingMiddleware>();
        services.AddScoped<RequestTimeMiddleware>();
    }

    public static void UseMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<RequestTimeMiddleware>();
    }
}