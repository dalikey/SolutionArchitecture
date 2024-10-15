using InvoiceManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagement.Infrastructure;

public class InvoiceMySQLContext : DbContext
{
    public InvoiceMySQLContext(DbContextOptions<InvoiceMySQLContext> options) : base(options) { }

    public DbSet<Invoice> Invoices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>().HasKey(x => x.InvoiceNumber);

        Invoice invoice = new Invoice()
        {
            InvoiceNumber = "1234",
            InvoiceDate = DateTime.Now,
            OrderNumber = "1",
            SupplierName = "Logitech",
            UserName = "John Doe"

        };

        Invoice invoice1 = new Invoice()
        {
            InvoiceNumber = "2345",
            InvoiceDate = DateTime.Now,
            OrderNumber = "2",
            SupplierName = "Pokemon",
            UserName = "Brock"
        };

        Invoice invoice2 = new Invoice()
        {
            InvoiceNumber = "3456",
            InvoiceDate = DateTime.Now,
            OrderNumber = "3",
            SupplierName = "RedBull",
            UserName = "Max Doe"
        };

        modelBuilder.Entity<Invoice>().HasData(
                       invoice, invoice1, invoice2
                              );
    }
}
