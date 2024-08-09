using System;
using System.ComponentModel.DataAnnotations;

namespace EXE201.DAL.DTOs.UserDTOs
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "User Name is required")]
        [MaxLength(50, ErrorMessage = "User Name can't be longer than 50 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [MaxLength(100, ErrorMessage = "Full Name can't be longer than 100 characters")]
        public string FullName { get; set; }
    }
}
