
using Microsoft.EntityFrameworkCore;

namespace labs5Prog.Models
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Currency> currenses { get; set; } = null!;
        public DbSet<Course> courses { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
