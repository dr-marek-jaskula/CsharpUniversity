using ASP.NETCoreWebAPI.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DecoratorsRegistration
{
    public static void RegisterServiceDecorators(this IServiceCollection services)
    {
        //Register decorators

        //Services
        services.Decorate<IOrderService, OrderServiceDecorator>();
    }
}