using System.ComponentModel.DataAnnotations;

namespace EASV.CustomerRestApi.Dtos
{
    public class RegisterDto
    {
        public string Username { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
    }
}