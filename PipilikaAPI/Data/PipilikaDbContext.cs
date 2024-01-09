using Microsoft.EntityFrameworkCore;
using PipilikaAPI.Models.Domain;

namespace PipilikaAPI.Data
{
    public class PipilikaDbContext : DbContext
    {

        public PipilikaDbContext(DbContextOptions<PipilikaDbContext> options):base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}
