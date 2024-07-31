using BusinessWebSoftPvtLmtTaskApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BusinessWebSoftPvtLmtTaskApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; } = null!;
    }
}
