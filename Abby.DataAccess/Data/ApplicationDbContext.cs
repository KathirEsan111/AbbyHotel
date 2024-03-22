
using Abby.Models;
using Microsoft.EntityFrameworkCore;

namespace AbbyWeb.DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Foodtype> Foodtype { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
    }
}
