using System.ComponentModel.DataAnnotations;

namespace liquorApi.Models;

    public class UserLoginDtoIn
    {
        [Required(ErrorMessage = "The email address is required")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; } = null!;
    }
