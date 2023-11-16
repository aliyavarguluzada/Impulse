namespace Impulse.DTOs.Vacancies
{
    public record VacancyDto
    {
        public int VacancyId { get; set; }
        public int JobTypeId { get; set; }
        public int JobCategoryId { get; set; }
        public int StatusId { get; set; }
        public int WorkFormId { get; set; }
        public string VacancyName { get; set; }
        public string CompanyName { get; set; }
        public string JobTypeName { get; set; }
        public string JobCategoryName { get; set; }
        public string WorkFormName { get; set; }
        public string CityName { get; set; }
        public string EducationName { get; set; }
        public string ExperienceName { get; set; }
        public string? Email { get; set; }
        public string Description { get; set; }
        public string CompanyLogoImage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
