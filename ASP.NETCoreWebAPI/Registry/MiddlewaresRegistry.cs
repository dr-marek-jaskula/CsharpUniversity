using ASP.NETCoreWebAPI.Middlewares;

namespace ASP.NETCoreWebAPI.Registry;

public static class MiddlewaresRegistry
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