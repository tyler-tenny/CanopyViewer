using Microsoft.EntityFrameworkCore;
using CanopyViewer.Models;

namespace CanopyViewer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine,
                Microsoft.Extensions.Logging.LogLevel.Information);
        }
    }
}
