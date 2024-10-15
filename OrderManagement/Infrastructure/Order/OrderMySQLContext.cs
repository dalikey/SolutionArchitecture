using Microsoft.EntityFrameworkCore;

namespace OrderManagement.Infrastructure.Order;

public class OrderMySQLContext : DbContext
{
    public OrderMySQLContext(DbContextOptions<OrderMySQLContext> options) : base(options) { }

    public DbSet<Domain.Order> Orders { get; set; }
    public DbSet<Domain.Product> Products { get; set; }
    public DbSet<Domain.User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Product
        modelBuilder.Entity<Domain.Product>().HasKey(p => p.ProductId);
        modelBuilder.Entity<Domain.Product>().HasIndex(p => p.ProductId).IsUnique();
        //autoincrement productid
        modelBuilder.Entity<Domain.Product>().Property(p => p.ProductId).ValueGeneratedOnAdd();

        Domain.Product RbWodka = new Domain.Product()
        {
            ProductId = 1,
            ProductName = "Red Bull Wodka",
            ProductDescription = "Energy drink with wodka",
            Price = 5,
            Category = "Energy drink"

        };
        Domain.Product RbWaterMelon = new Domain.Product()
        {
            ProductId = 2,
            ProductName = "Red Bull Watermelon",
            ProductDescription = "Energy drink with watermelon",
            Price = 3,
            Category = "Energy drink"

        };
        Domain.Product RbGrapefruit = new Domain.Product()
        {
            ProductId = 3,
            ProductName = "Red Bull Grapefruit",
            ProductDescription = "Energy drink with grapefruit",
            Price = 4,
            Category = "Energy drink"
        };

        modelBuilder.Entity<Domain.Product>().HasData(RbWodka, RbWaterMelon, RbGrapefruit);

        Domain.Product G603 = new Domain.Product()
        {
            ProductId = 4,
            ProductName = "Logitech G603",
            ProductDescription = "Wireless gaming mouse",
            Price = 70,
            Category = "Energy drink"
        };
        Domain.Product GPro = new Domain.Product()
        {
            ProductId = 5,
            ProductName = "Logitech G Pro",
            ProductDescription = "Wired gaming mouse",
            Price = 50,
            Category = "Energy drink"
        };
        Domain.Product G213 = new Domain.Product()
        {
            ProductId = 6,
            ProductName = "Logitech G213",
            ProductDescription = "Gaming keyboard",
            Price = 40,
            Category = "Energy drink"

        };

        modelBuilder.Entity<Domain.Product>().HasData(G603, GPro, G213);

        Domain.Product pikachu = new Domain.Product()
        {
            ProductId = 7,
            ProductName = "Pikachu",
            ProductDescription = "Electric pokemon",
            Price = 100,
            Category = "Energy drink"
        };

        Domain.Product snorlax = new Domain.Product()
        {
            ProductId = 8,
            ProductName = "Snorlax",
            ProductDescription = "Sleeping pokemon",
            Price = 200,
            Category = "Energy drink"
        };

        Domain.Product charizard = new Domain.Product()
        {
            ProductId = 9,
            ProductName = "Charizard",
            ProductDescription = "Fire pokemon",
            Price = 150,
            Category = "Energy drink"
        };

        modelBuilder.Entity<Domain.Product>().HasData(pikachu, snorlax, charizard);

        // User
        modelBuilder.Entity<Domain.User>().HasKey(o => o.UserId);
        modelBuilder.Entity<Domain.User>().HasIndex(o => o.UserId).IsUnique();

        var Logitech = new Domain.User
        {
            UserId = 1,
            Email = "Logitech@mail.com",
            FirstName = "John",
            LastName = "Doe"
        };

        var Pokemon = new Domain.User
        {
            UserId = 2,
            Email = "Pokemon@mail.com",
            FirstName = "Ash",
            LastName = "Ketchum"

        };

        var RedBull = new Domain.User
        {
            UserId = 3,
            Email = "Redbull@mail.com",
            FirstName = "Max",
            LastName = "Verstappen"
        };

        modelBuilder.Entity<Domain.User>().HasData(
                Logitech, Pokemon, RedBull
            );

        // Order
        modelBuilder.Entity<Domain.Order>().HasKey(o => o.OrderNumber);
        modelBuilder.Entity<Domain.Order>().HasIndex(o => o.OrderNumber).IsUnique();

        // Order - Products
        modelBuilder.Entity<Domain.Order>().HasMany(o => o.Products).WithMany(p => p.Orders);

        // Order - User
        modelBuilder.Entity<Domain.Order>().HasOne(o => o.User).WithMany(u => u.Orders);

        var order1 = new Domain.Order
        {
            OrderNumber = "1",
            OrderDate = DateTime.Now,
            Status = "Delivered",
            UserId = 1,
            SupplierName = "Logitech"
        };

        var order2 = new Domain.Order
        {
            OrderNumber = "2",
            OrderDate = DateTime.Now,
            Status = "Processing",
            UserId = 2,
            SupplierName = "Pokemon"
        };

        var order3 = new Domain.Order
        {
            OrderNumber = "3",
            OrderDate = DateTime.Now,
            Status = "Shipped",
            UserId = 3,
            SupplierName = "RedBull"
        };

        modelBuilder.Entity<Domain.Order>().HasData(
                order1, order2, order3
            );

        // Order - Products
        modelBuilder.Entity<Domain.Order>().HasMany(o => o.Products).WithMany(p => p.Orders)
            .UsingEntity<Dictionary<string, object>>(
                "OrderProduct",
                j => j.HasOne<Domain.Product>().WithMany().HasForeignKey("ProductsProductId"),
                j => j.HasOne<Domain.Order>().WithMany().HasForeignKey("OrdersOrderNumber"),
                j =>
                {
                    j.Property<string>("OrdersOrderNumber");
                    j.Property<int>("ProductsProductId");
                    j.HasData(
                        new { OrdersOrderNumber = "1", ProductsProductId = 1 },
                        new { OrdersOrderNumber = "1", ProductsProductId = 2 },
                        new { OrdersOrderNumber = "2", ProductsProductId = 2 },
                        new { OrdersOrderNumber = "2", ProductsProductId = 3 },
                        new { OrdersOrderNumber = "3", ProductsProductId = 1 }
                    );
                });

        // Order - User
        modelBuilder.Entity<Domain.Order>().HasOne(o => o.User).WithMany(u => u.Orders);
    }
}
