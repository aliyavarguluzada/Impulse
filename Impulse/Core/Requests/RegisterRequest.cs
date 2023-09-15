using System.ComponentModel.DataAnnotations;

namespace Impulse.Core.Requests
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Ad boş qala bilməz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-poçt boş qala bilməz.")]
        [EmailAddress(ErrorMessage = "Düzgün e-poçt daxil edin.")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrə boş qala bilməz.")]
        [StringLength(16, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]

        public string Password { get; set; }


        [Required(ErrorMessage = "Təkrar şifrə boş qala bilməz.")]
        [Compare("Password", ErrorMessage = "Təkrar şifrə düzgün deyil.")]
        [StringLength(16, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]

        public string ConfirmPassword { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
