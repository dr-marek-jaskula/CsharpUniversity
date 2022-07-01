using ASP.NETCoreWebAPI.Services;
using ASP.NETCoreWebAPI.Services.Decorators;

namespace Microsoft.Extensions.DependencyInjection;

public static class DecoratorsRegistration
{
    public static void RegisterServiceDecorators(this IServiceCollection services)
    {
        //Register decorators

        //Services
        services.Decorate<IOrderService, OrderServiceDecorator>();
        services.Decorate<IDecoratedGitHubService, GitHubServicePollyDecorator>();
    }
}