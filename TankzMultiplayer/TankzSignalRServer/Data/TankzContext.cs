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
        public DbSet<Match> Matches { get; set; }
        public DbSet<Tank> Tanks { get; set; }
        public DbSet<TankState> TankStates { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Crate> Crates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Match>().ToTable("Match");
            modelBuilder.Entity<Tank>().ToTable("Tank");
            modelBuilder.Entity<TankState>().ToTable("TankState");
            modelBuilder.Entity<Weapon>().ToTable("Weapon");
            modelBuilder.Entity<Crate>().ToTable("Crate");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Map>().ToTable("Map");
            modelBuilder.Entity<Lobby>().ToTable("Lobby");
        }
    }
}
