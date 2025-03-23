using Microsoft.EntityFrameworkCore;

namespace UniversityAPI.Models
{
    public class UniversityContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
        }
        
    }
 
}
