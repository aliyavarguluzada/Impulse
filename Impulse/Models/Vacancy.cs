using Microsoft.Identity.Client;

namespace Impulse.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int JobTypeId { get; set; }
        public int WorkFormId { get; set; }
        public int CityId { get; set; }
        public int JobCategoryId { get; set; }
        public int EducationId { get; set; }
        public string? Email { get; set; }
        public string Description { get; set; }
        public string CompanyLogoImage { get; set; }
        public string Experience { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Company Company { get; set; }
        public virtual JobType JobType { get; set; }
        public virtual WorkForm WorkForm { get; set; }
        public virtual City City { get; set; }
        public virtual JobCategory JobCategory { get; set; }
        public virtual Education Education { get; set; }
    }
}
