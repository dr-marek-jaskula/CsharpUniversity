using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPDotNetLearningApplication
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int RoleId { get; set; } = 1;
    }
}


/* Json to register
 {
    "email": "whyitsnot@test.com",
    "password": "notworking",
    "confirmPassword": "notworking",
    "nationality": "Poland",
    "DateOfBirth": "1991-06-18"
}
 */