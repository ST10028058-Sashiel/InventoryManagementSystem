using Microsoft.EntityFrameworkCore;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Electronics> Electronics { get; set; }
        public DbSet<Grocery> Groceries { get; set; }
        public DbSet<Clothing> Clothings { get; set; }
    }
}
