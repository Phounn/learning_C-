using Microsoft.EntityFrameworkCore;
using WebApi_2.Models;
namespace WebApi_2.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Employee> Employees => Set<Employee>();
    }
}
