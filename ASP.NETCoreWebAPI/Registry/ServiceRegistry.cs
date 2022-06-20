using ASP.NETCoreWebAPI.Services;

namespace ASP.NETCoreWebAPI.Registry;

public static class ServiceRegistry
{
    public static void RegisterServices(this IServiceCollection services)
    {
        //Services
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IGitHubService, GitHubService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<INoThrowService, NoThrowService>();

        //Background Services
        services.AddHostedService<RepetingBackgroundService>(); //Uncomment to demonstrate PeriodicTimer
    }
}