using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Impulse.Core.Requests
{
    public class AdminRegisterRequest
    {

        [Required(ErrorMessage = "Ad boş qala bilməz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-poçt boş qala bilməz.")]
        [EmailAddress(ErrorMessage = "Düzgün e-poçt daxil edin.")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Düzgün e-poçt daxil edin.")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrə boş qala bilməz.")]
        [StringLength(16, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }


        public string? Phone { get; set; }

        [Required(ErrorMessage = "Təkrar şifrə boş qala bilməz.")]
        [Compare("Password", ErrorMessage = "Təkrar şifrə düzgün deyil.")]
        [StringLength(16, ErrorMessage = "Must be between 8 and 16 characters", MinimumLength = 8)]

        public string ConfirmPassword { get; set; }

        public string? ReturnUrl { get; set; }

       
    }
}
