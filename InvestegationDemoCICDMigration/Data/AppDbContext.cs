using Microsoft.EntityFrameworkCore;
using DevOpsWebApp.Models;

namespace DevOpsWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees => Set<Employee>();
    }
}
