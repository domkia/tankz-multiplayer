using Microsoft.EntityFrameworkCore;
using TankzSignalRServer.Models;

namespace TankzSignalRServer.Data
{
    public class TankzContext : DbContext
    {
        public TankzContext(DbContextOptions<TankzContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
        }
    }
}
