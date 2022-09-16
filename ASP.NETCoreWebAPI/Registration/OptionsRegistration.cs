using ASP.NETCoreWebAPI.Options;
using Microsoft.Extensions.Options;

namespace ASP.NETCoreWebAPI.Registration;

//We use the Option Pattern (modern way) to apply options setup
//We obtain:
//1. A way to validate options at startup in a clean and loosely coupled way
//2. We just need to change appsettings.json and rerun app, rather then rebuild and redeploy the application
public static class OptionsRegistration
{
    public static void RegisterOptions(this IServiceCollection services)
    {
        //We configure the Options Setup, if some validators are registered, then the validation will be performed at start.
        services.ConfigureOptions<DatabaseOptionsSetup>();

        //We register the Options Validator
        services.AddSingleton<IValidateOptions<DatabaseOptions>, DatabaseOptionsValidator>();
    }
}