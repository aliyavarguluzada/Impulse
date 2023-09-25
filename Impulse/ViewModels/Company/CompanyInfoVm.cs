using Impulse.DTOs.CompanyInfo;
using System.Net;

namespace Impulse.ViewModels.Company
{
    public class CompanyInfoVm
    {
        public List<CityDto> cities { get; set; }
        public List<EducationDto> educations { get; set; }
        public List<JobCategoryDto> jobCategories { get; set; }
        public List<WorkFormDto> workForms { get; set; }
        public List<JobTypeDto> jobTypes { get; set; }
        public List<ExperienceDto> experiences { get; set; }

    }
}
