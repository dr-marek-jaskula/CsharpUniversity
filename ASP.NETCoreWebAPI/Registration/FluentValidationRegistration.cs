using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class FluentValidationRegistration
{
    public static void RegisterFluentValidation(this IServiceCollection services)
    {
        services
            //.AddFluentValidation(options => //Fluent Validation (Models -> Validators)
            .AddFluentValidationAutoValidation(options => //Fluent Validation (Models -> Validators)
            {
                //To disable the default Mvc validation
                options.DisableDataAnnotationsValidation = true;

                //To validate child properties, we need to use "SetValidator" in the rules. See Models -> Validators

                //To disable error messages in a local language. Default one is English
                ValidatorOptions.Global.LanguageManager.Enabled = false;
                //To force error messages in a certain language
                //ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("fr");
            })
            //Automatic registration of validators in assembly (therefore there is no need to register validators below)
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
