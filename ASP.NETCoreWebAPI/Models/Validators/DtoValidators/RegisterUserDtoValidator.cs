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
            .WithMessage("{PropertyName} address can not be null, empty string or whitespace only")
            //Property name will be placed there.
            //Other general placeholder: {PropertyValue}
            .EmailAddress()
            .WithMessage("A valid {PropertyName} address is required")
            //Custom validation using lambda expression. We inject context to examine if given email is already in the database
            .Custom((value, context) => //value is the value of the email property
            {
                if (dbContext.Users.Any(u => u.Email == value))
                    context.AddFailure("Email", "That email is taken");
            });

        RuleFor(ru => ru.Password)
            .MinimumLength(6)
            .WithMessage("{PropertyName} needs to be at least {MinLength} character")
            .MaximumLength(32)
            .WithMessage("{PropertyName} needs to be at most {MaxLength} character")
            .Must(IsPasswordValid)
            .WithMessage("{PropertyName} needs to contain at least one digit, one small letter and one capital letter.");

        RuleFor(ru => ru.ConfirmPassword)
            .Equal(re => re.Password)
            .WithMessage("{PropertyName} failed");
    }

    private bool IsPasswordValid(string? password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        Regex regex = new(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{0,}$", 
            //This option boots the performance of the regex
            RegexOptions.Compiled);
        return regex.IsMatch(password);
    }
}