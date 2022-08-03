using ASP.NETCoreWebAPI.Helpers;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using ASP.NETCoreWebAPI.Models.Validators.CustomValidators;
using FluentValidation;

namespace ASP.NETCoreWebAPI.Models.Validators;

public class OrderDtoValidator : AbstractValidator<OrderDto>
{
    public OrderDtoValidator(IDateTimeProvider dateTimeProvider)
    {
        //Add the custom validation method
        RuleFor(o => o.Deadline).AfterSunrise(dateTimeProvider);

        //Add another validator
        RuleFor(o => o.Payment).SetValidator(new PaymentValidator());
    }
}