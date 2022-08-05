using ASP.NETCoreWebAPI.Attributes;
using ASP.NETCoreWebAPI.Helpers;
using ASP.NETCoreWebAPI.Logging;
using ASP.NETCoreWebAPI.Repositories;
using ASP.NETCoreWebAPI.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        //Registering a service is making a ServiceDescriptor (a plan how to resolve a service)
        //an example (manual):
        //var addresServiceDescriptor = new ServiceDescriptor(typeof(IAddressService), typeof(AddressService), ServiceLifetime.Scoped);
        //services.Add(addresServiceDescriptor);
        //We can also use "TryAdd" if we want to avoid duplicates (from "Microsoft.Extensions.DependencyInjection.Extensions")

        //Services
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IGitHubService, GitHubService>();
        services.AddScoped<INoThrowService, NoThrowService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IDecoratedGitHubService, DecoratedGitHubService>();
        services.AddScoped<IRefitGitHubService, RefitGitHubService>();

        //services.AddSingleton<OutputFormatterSelector, AcceptHeaderOutPutFormatterSelector>();

        #region Scrutor Scanning approach

        //Register service by scanning
        //My preferred way for scanning is to use Scrutor build in "ServiceDescriptor" attribute
        //This attribute looks like:
        //[ServiceDescriptor(typeof(IExampleAService), ServiceLifetime.Singleton)]
        //This attribute should be used on each service I want to register later (Nevertheless, this approach was not used here)

        //services.Scan(selector =>
        //{
        //    selector.FromAssemblyOf<Program>()
        //        .AddClasses()
        //            .UsingAttributes(); //use default Scrutor "ServiceDescriptor" attribute
        //});

        //The problem with default "ServiceDescriptor" attribute marker method is:
        //"Should the class itself knows how to register it?"

        //Alternative is to use:
        //.AddClasses(filter => filter.InNamespaces("ASP.NETCoreWebAPI.Services"))

        #endregion Scrutor Scanning approach

        //The last service registered will be resolved in case when we register two different implementation of a single interface
        //(if we inject a single instance of a service)!!!!
        //However, if we inject a collection (IEnumerable<InterfaceType>), then all of the registered implementations will be present (we can resolve any of implementation)

        //Repositories
        services.AddScoped<IAddressRepository, AddressRepository>();

        //Register helpers
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        //Register open generics in a proper way:
        services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

        //Background Services (also called "HostedServices")
        services.AddHostedService<RepetingBackgroundService>(); //Uncomment to demonstrate PeriodicTimer
    }
}