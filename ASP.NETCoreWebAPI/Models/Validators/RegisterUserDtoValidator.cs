using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using EFCore;
using FluentValidation;

namespace ASP.NETCoreWebAPI.Models.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator(MyDbContext dbContext)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password).MinimumLength(6);

        RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

        //Custom validation using lambda expression. We incject context to examine if given email is already in the database
        RuleFor(x => x.Email).Custom((value, contex) =>
        {
            bool emialInUse = dbContext.Users.Any(u => u.Email == value);
            if (emialInUse)
                contex.AddFailure("Email", "That email is taken");
        });
    }

}
