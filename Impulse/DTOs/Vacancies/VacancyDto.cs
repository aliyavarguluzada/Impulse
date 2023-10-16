namespace Impulse.DTOs.Vacancies
{
    public class VacancyDto
    {
        public int VacancyId { get; set; }
        public string VacancyName { get; set; }
        public int? CompanyId { get; set; }
        public int JobTypeId { get; set; }
        public int JobCategoryId { get; set; }
        public int WorkFormId { get; set; }
        public int CityId { get; set; }
        public int EducationId { get; set; }
        public int ExperienceId { get; set; }
        public string? Email { get; set; }
        public string Description { get; set; }
        public string CompanyLogoImage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
