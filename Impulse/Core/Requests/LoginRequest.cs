using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Impulse.Core.Requests
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Emailin strukturu yanlışdır")]

        public string Email { get; set; }

        [Required]
        [PasswordPropertyText]
        [StringLength(16, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]

        public string Password { get; set; }
    }
}
