﻿using ASP.NETCoreWebAPI.Logging;
using ASP.NETCoreWebAPI.Services;
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        //Registering a service is making a ServiceDescriptor (a plan how to resolve a service)
        //an example (manual):
        //var addresServiceDescriptor = new ServiceDescriptor(typeof(IAddressService), typeof(AddressService), ServiceLifetime.Scoped);
        //services.Add(addresServiceDescriptor);

        //Services
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IGitHubService, GitHubService>();
        services.AddScoped<INoThrowService, NoThrowService>();
        services.AddScoped<IAddressService, AddressService>();
        
        //The last service registered will be resolved in case when we register two different implementation of a single interface
        //(if we inject a single instance of a service)!!!!
        //However, if we inject a collection (IEnumerable<InterfaceType>), then all of the registered implementations will be present (we can resolve any of implementation)

        //Register open generics in a proper way: 
        services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

        //Background Services (also called "HostedServices")
        services.AddHostedService<RepetingBackgroundService>(); //Uncomment to demonstrate PeriodicTimer
    }
}