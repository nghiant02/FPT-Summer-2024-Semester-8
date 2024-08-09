using System.ComponentModel.DataAnnotations;

namespace EXE201.ViewModel.UserViewModel
{
    public class LoginUserDTOs
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
