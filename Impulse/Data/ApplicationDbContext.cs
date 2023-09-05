using Microsoft.EntityFrameworkCore;

namespace Impulse.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } 

         // TODO: Models elave edilmelidi  
    }
}
