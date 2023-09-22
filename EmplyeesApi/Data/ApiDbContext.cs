using EmplyeesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmplyeesApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> VbEmployees { get; set; }
    }
}
