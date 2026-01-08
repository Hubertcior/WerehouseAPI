using Microsoft.EntityFrameworkCore;
using WerehouseAPI.Entities;

namespace WerehouseAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<PackageStatus> PackageStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PackageStatus>().HasData(
            new PackageStatus { Id = 1, Name = "Created" },
            new PackageStatus { Id = 2, Name = "InTransit" },
            new PackageStatus { Id = 3, Name = "Delivered" }
            );
        }
    }
}
