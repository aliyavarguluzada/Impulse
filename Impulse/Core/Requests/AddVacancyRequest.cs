using System.ComponentModel.DataAnnotations;

namespace Impulse.Core.Requests
{
    public class AddVacancyRequest
    {
        [Required]
        public string VacancyName { get; set; }

        [Required]

        public string Description { get; set; }

        [Required]

        public string Logo { get; set; }

        [Required(ErrorMessage = "Düzgün Email daxil edin")]
        [EmailAddress(ErrorMessage = "Düzgün Email daxil edin")]
        public string Email { get; set; }
    }
}
