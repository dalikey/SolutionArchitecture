using Microsoft.EntityFrameworkCore;

namespace OrderManagement.Infrastructure.Events;

public class EventsMySQLContext : DbContext
{
    public EventsMySQLContext(DbContextOptions<EventsMySQLContext> options) : base(options){ }

    public DbSet<Domain.Events.OrderEvent> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Event
        modelBuilder.Entity<Domain.Events.OrderEvent>().HasKey(e => e.EventId);
        modelBuilder.Entity<Domain.Events.OrderEvent>().Property(e => e.EventId).ValueGeneratedOnAdd();
        modelBuilder.Entity<Domain.Events.OrderEvent>().HasIndex(e => e.AggregateId);
        modelBuilder.Entity<Domain.Events.OrderEvent>().Property(e => e.EventData).IsRequired();
        modelBuilder.Entity<Domain.Events.OrderEvent>().Property(e => e.EventTime).IsRequired();
    }
}
