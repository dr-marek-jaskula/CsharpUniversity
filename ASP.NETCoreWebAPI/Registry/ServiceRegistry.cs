﻿using ASP.NETCoreWebAPI.Services;

namespace ASP.NETCoreWebAPI.Registry;

public static class ServiceRegistry
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IGitHubService, GitHubService>();
        services.AddScoped<IAddressService, AddressService>();
    }
}