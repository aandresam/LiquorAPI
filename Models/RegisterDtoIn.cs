using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace liquorApi.Models
{
    public class RegisterDtoIn
    {
        [
            Required(ErrorMessage = "The email address is required"),
            EmailAddress(ErrorMessage = "The email address format is ivalid")
        ]
        public string Email { get; set; } = null!;

        [
            Required(ErrorMessage = "The password is required"),
            MinLength(10, ErrorMessage = "The password length must be at least 10 characters long")
        ]
        public string Password { get; set; } = null!;
    }
}