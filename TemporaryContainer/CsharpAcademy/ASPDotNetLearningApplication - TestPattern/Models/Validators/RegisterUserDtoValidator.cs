using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ASPDotNetLearningApplication
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress(); //niepuste i format email

            RuleFor(x => x.Password).MinimumLength(6); //minimalna d³ugoœæ

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password); //porównuje Password z Confirm Password

            //customowa validacja za pomoc¹ lambdy. Wstrzykujemy kontekst aby sprawdziæ czy email ju¿ jest w bazie danych
            RuleFor(x => x.Email).Custom((value, contex) =>
            {
                bool emialInUse = dbContext.Users.Any(u => u.Email == value);
                if (emialInUse)
                    contex.AddFailure("Email", "That email is taken");
            });
        }

    }
}
