namespace Impulse.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserStatusId { get; set; }
        public int CompanyId { get; set; }
        public int JobTypeId { get; set; }
        public int WorkFormId { get; set; }
        public int CityId { get; set; }
        public int JobCategoryId { get; set; }
        public int EducationId { get; set; }
        public string Description { get; set; }
        public string Company {  get; set; }
        public string CompanyLogoImage { get; set; }
        public string Experience { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
