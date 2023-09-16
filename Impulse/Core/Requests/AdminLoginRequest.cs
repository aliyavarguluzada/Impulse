using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Impulse.Core.Requests
{
    public class AdminLoginRequest
    {
        [Required]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Emailin strukturu yanlışdır")]

        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        [StringLength(16, ErrorMessage = "Must be between 8 and 16 characters", MinimumLength = 8)]

        public string Password { get; set; }
    }
}
