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
