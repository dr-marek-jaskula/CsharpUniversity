using ASP.NETCoreWebAPI.Middlewares;

namespace Microsoft.Extensions.DependencyInjection;

public static class MiddlewaresRegistration
{
    public static void RegisterMiddlewares(this IServiceCollection services)
    {
        //Order is not important
        services.AddScoped<ErrorHandlingMiddleware>();
        services.AddScoped<RequestTimeMiddleware>();
    }

    public static void UseMiddlewares(this WebApplication app)
    {
        //Order is important
        app.UseMiddleware<NameRoutingMiddleware>(); //Do not register it before (without IMiddleware interface)
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware<RequestTimeMiddleware>();
    }
}