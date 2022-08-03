using ASP.NETCoreWebAPI.Helpers;
using FluentValidation;

namespace ASP.NETCoreWebAPI.Models.Validators.CustomValidators;

public static class DateTimeValidators
{
    public static IRuleBuilderOptions<T, DateTime> AfterSunrise<T>(this IRuleBuilder<T, DateTime> ruleBuilder, IDateTimeProvider dateTimeProvider)
    {
        var sunrise = TimeOnly.FromDateTime(dateTimeProvider.Midnight.AddHours(6));

        return ruleBuilder
            .Must((objectRoot, dateTime, context) =>
            {
                TimeOnly providedTime = TimeOnly.FromDateTime(dateTime);

                //Add custom placeholders (they will be replace if there are present in the error message)
                context.MessageFormatter.AppendArgument("Sunrise", sunrise);
                context.MessageFormatter.AppendArgument("ProvidedTime", providedTime);

                return providedTime > sunrise;
            })
            .WithMessage("{PropertyName} must be after {Sunrise}. You provided {ProvidedTime}");
    }
}
