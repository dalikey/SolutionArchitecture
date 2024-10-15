using Microsoft.EntityFrameworkCore;
using SupportManagement.Domain;

namespace SupportManagement.Infrastructure;

public class SupportMySQLContext : DbContext
{
    public SupportMySQLContext(DbContextOptions<SupportMySQLContext> options) : base(options) { }

    public DbSet<Support> Supports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Create unique index on Support.SupportId
        modelBuilder.Entity<Support>().HasKey(s => s.SupportId);
        modelBuilder.Entity<Support>().HasIndex(s => s.SupportId).IsUnique();

        var ST1001 = new Support
        {
            SupportId = 1,
            SupportTicketNumber = "ST-1001",
            UserEmail = "user1@example.com",
            IssueDate = DateTime.Now,
            Status = "Open",
            Description = "Unable to login to the account."
        };

        modelBuilder.Entity<Support>().HasData(
                ST1001
            );
    }
}
