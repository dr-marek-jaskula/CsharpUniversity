using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WzorceProjektowe
{
    class SingleResponsibility
    {
        //"Every software module should have only one reason to change"
        // Every class or similar structure should have only one job to do.
    }

    class GoodExample
    {
        EmailValidator _emailValidator;
        string _password;

        public void Register(string email, string password)
        {
            if (!_emailValidator.ValidateEmail(email)) throw new ValidationException("Email is not an email");
            _password = password;
        }

        public GoodExample(EmailValidator emailValidator)
        {
            _emailValidator = emailValidator;
        }
    }

    class BadExample
    {
        string _password;
        public void Register(string email, string password)
        {
            if (!ValidateEmail(email)) throw new ValidationException("Email is not an email");
            _password = password;
        }

        public bool ValidateEmail(string input)
        {
            return input.Contains("@");
        }

        // this class is responsible for two different thing at the highest level: Registering and Validating.
        // It would be ok if the responsibility for validating the email was set to the child class EmailValidator, because then the single responsibility would hold
    }

    class EmailValidator
    {
        public bool ValidateEmail(string input)
        {
            return input.Contains("@");
        }
    }
}
