using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using FluentValidation;

namespace ASP.NETCoreWebAPI.Models.Validators;

public class PaymentValidator : AbstractValidator<PaymentDto>
{
    public PaymentValidator()
    {
        RuleFor(x => x.Total).GreaterThan(0);
        RuleFor(x => x.Status).NotEmpty();
    }
}
