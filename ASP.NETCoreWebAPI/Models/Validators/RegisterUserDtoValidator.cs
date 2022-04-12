using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using EFCore;
using FluentValidation;

namespace ASP.NETCoreWebAPI.Models.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{

     
    public RegisterUserDtoValidator(MyDbContext dbContext)
    {
        RuleFor(ru => ru.Email)
            .NotEmpty();
        //    .WithMessage("Email address is required")
        //    .EmailAddress()
        //    .WithMessage("A valid email address is required")
        //     //Custom validation using lambda expression. We inject context to examine if given email is already in the database
        //    .Custom((value, contex) => //value is the value of the email
        //    {
        //         if (dbContext.Users.Any(u => u.Email == value))
        //             contex.AddFailure("Email", "That email is taken");
        //    });

        //RuleFor(ru => ru.Password)
        //    .MinimumLength(6)
        //    .WithMessage("Password needs to be at least 6 character");

        //RuleFor(ru => ru.ConfirmPassword)
        //    .Equal(re => re.Password)
        //    .WithMessage("Incorrect password");
    }
}