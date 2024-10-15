using Microsoft.EntityFrameworkCore;
using TrackingManagement.Domain;

namespace TrackingManagement.Infrastructure;

public class TrackingMySQLContext : DbContext
{
    public TrackingMySQLContext(DbContextOptions<TrackingMySQLContext> options) : base(options) { }

    public DbSet<TrackingData> Trackings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TrackingData>().HasKey(t => t.TrackingId);
        modelBuilder.Entity<TrackingData>().HasIndex(t => t.TrackingId).IsUnique();
    }
}
