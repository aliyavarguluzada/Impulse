using Impulse.DTOs.CompanyInfo;
using Impulse.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Impulse.Core.Requests
{
    public class AddVacancyRequest
    {
        [Required]
        public string VacancyName { get; set; }

        [Required]

        public string Description { get; set; }

        [Required(ErrorMessage = "Düzgün Email daxil edin")]
        [EmailAddress(ErrorMessage = "Düzgün Email daxil edin")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Logo üçün fayl daxil edin")]
        public IFormFile Logo { get; set; }

        //public List<WorkFormDto> WorkForms { get; set; }
        //public List<JobTypeDto> JobTypes { get; set; }
        //public List<JobCategoryDto> JobCategories { get; set; }
        //public List<CityDto> Cities { get; set; }
        //public List<ExperienceDto> Experiences { get; set; }
        //public List<EducationDto> Educations { get; set; }

        public int WorkFormId { get; set; }
        public int JobTypeId { get; set; }
        public int JobCategoryId { get; set; }
        public int CityId { get; set; }
        public int EducationId { get; set; }
        public int ExperienceId { get; set; }


    }
}
