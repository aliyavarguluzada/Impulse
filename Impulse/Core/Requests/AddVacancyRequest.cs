using Impulse.DTOs.CompanyInfo;
using Impulse.Models;
using System.ComponentModel.DataAnnotations;

namespace Impulse.Core.Requests
{
    public class AddVacancyRequest
    {
        [Required]
        public string VacancyName { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]

        public string Description { get; set; }

        [Required(ErrorMessage = "Düzgün Email daxil edin")]
        [EmailAddress(ErrorMessage = "Düzgün Email daxil edin")]
        public string Email { get; set; }

        public int JobTypeId { get; set; }

        public int JobCategoryId { get; set; }
        public int WorkFormId { get; set; }
        public int City { get; set; }
        public int EducationId { get; set; }
        public int ExperienceId { get; set; }
        public IFormFile Logo { get; set; }

    }
}
