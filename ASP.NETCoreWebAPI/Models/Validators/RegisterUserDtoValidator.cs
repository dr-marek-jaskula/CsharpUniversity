using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using EFCore;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ASP.NETCoreWebAPI.Models.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator(MyDbContext dbContext)
    {
        RuleFor(ru => ru.Email)
            .NotEmpty()
            //Override the default error message
            .WithMessage("{PropertyName} address is required")
            //Property name will be placed there.
            //Other general placeholder: {PropertyValue}
            .EmailAddress()
            .WithMessage("A valid {PropertyName} address is required");
            //Custom validation using lambda expression. We inject context to examine if given email is already in the database
            //.Custom((value, context) => //value is the value of the email property
            //{
            //    if (dbContext.Users.Any(u => u.Email == value))
            //        contex.AddFailure("{PropertyName}", "That {PropertyName} is taken");
            //});

        RuleFor(ru => ru.Password)
            //.MinimumLength(6)
            //.WithMessage("{PropertyName} needs to be at least {MinLength} character")
            .Must(IsPasswordValid)
            .WithMessage("{PropertyName} needs to be 6-32 character long, contain at least one number, one small letter and one capital letter.");

        RuleFor(ru => ru.ConfirmPassword)
            .Equal(re => re.Password)
            .WithMessage("{PropertyName} failed");
    }

    private bool IsPasswordValid(string password)
    {
        Regex regex = new(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,32}$");
        return regex.IsMatch(password);
    }
}