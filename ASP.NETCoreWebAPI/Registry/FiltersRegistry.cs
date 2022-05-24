using ASP.NETCoreWebAPI.Filters;

namespace ASP.NETCoreWebAPI.Registry;

//Filters are like Middlewares but for the specific endpoint or controller
//In order to apply filters is to use "ServiceFilter" on the certain controller action
public static class FiltersRegistry
{
    public static void RegisterFilters(this IServiceCollection services)
    {
        //Validation filter
        services.AddMvc(opt =>
        {
            opt.Filters.Add(typeof(ValidatorActionFilter));
        });

        //Add Logging filter
        services.AddScoped<LoggingFilter>();
    }
}