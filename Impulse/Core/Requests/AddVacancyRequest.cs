using System.ComponentModel.DataAnnotations;

namespace Impulse.Core.Requests
{
    public class AddVacancyRequest
    {
        [Required]
        public string VacancyName { get; set; }

        public string CompanyName { get; set; }

        [Required]

        public string Description { get; set; }

        [Required(ErrorMessage = "Email daxil edin")]
        [EmailAddress(ErrorMessage = "Format düzgün deyil")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Logo üçün fayl daxil edin")]
        public IFormFile Logo { get; set; }

        [Required]
        public int WorkFormId { get; set; }

        [Required]
        public int JobTypeId { get; set; }

        [Required]
        public int JobCategoryId { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public int EducationId { get; set; }

        [Required]
        public int ExperienceId { get; set; }


    }
}
