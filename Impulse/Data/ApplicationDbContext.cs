using Impulse.Models;
using Microsoft.EntityFrameworkCore;

namespace Impulse.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // TODO: Models elave edilmelidi
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanySocial> CompanySocials { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<Cv> Cvs { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<WorkForm> WorkForms { get; set; }
        public DbSet<SecondarySiteSettings> SecondarySiteSettings { get; set; }
    }
}
