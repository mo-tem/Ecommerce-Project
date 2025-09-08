using Microsoft.EntityFrameworkCore;  // EF Core namespace, provides DbContext and DbSet
using EcomApi.Models;                 // Including Order model so EF Core knows about it

namespace EcomApi.Data
{
    // DbContext represents a session with the database
    // It lets you query and save instances of your entities (models)
    public class AppDbContext : DbContext
    {
        // Constructor: passes options (like connection string) to base DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet represents a table in the database
        // Here, Orders table corresponds to Order model
        public DbSet<Order> Orders { get; set; }
    }
}
