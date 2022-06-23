using ASP.NETCoreWebAPI.Filters;

namespace Microsoft.Extensions.DependencyInjection;

//Filters are like Middlewares but for the specific endpoint or controller
//In order to apply filters is to use "ServiceFilter" on the certain controller action
public static class FiltersRegistration
{
    public static void RegisterFilters(this IServiceCollection services)
    {
        services.AddMvc(opt =>
        {
            opt.Filters.Add(typeof(ValidatorActionFilter));
        });

        services.AddScoped<LoggingFilter>();
        services.AddScoped<DurationLoggerFilter>();
    }
}