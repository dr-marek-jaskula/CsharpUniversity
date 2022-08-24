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

        //Add another validator -> this is proper approach for validating complex properties
        RuleFor(o => o.Payment).SetValidator(new PaymentValidator());

        //Other Fluent Validator options (validation for each element of a collection):
        //RuleForEach(o => o.SubItems)
        //    .NotEmpty()
        //    .WithMessage("Values in the SubItems array cannot be empty");

        //Conditional Validation 
        //When(t => t.RemindMe == true, () =>
        //{
        //    RuleFor(t => t.ReminderMinutesBeforeDue)
        //        .NotNull()
        //        .WithMessage("ReminderMinutesBeforeDue must be set")
        //        .GreaterThan(0)
        //        .WithMessage("ReminderMinutesBeforeDue must be greater than 0")
        //        .Must(value => value % 15 == 0)
        //        .WithMessage("ReminderMinutesBeforeDue must be a multiple of 15");
        //});
    }
}