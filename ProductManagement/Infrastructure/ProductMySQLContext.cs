using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain;

namespace ProductManagement.Infrastructure;

public class ProductMySQLContext : DbContext
{
    public ProductMySQLContext(DbContextOptions<ProductMySQLContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Create unique index on Product.ProductId
        modelBuilder.Entity<Product>().HasKey(p => p.ProductId);

        Product RbWodka = new Product()
        {
            ProductId = 1,
            ProductName = "Red Bull Wodka",
            ProductDescription = "Energy drink with wodka",
            Price = 5,
            StockQuantity = 10,
            Category = "Drinks",
            SupplierId = 3
        };
        Product RbWaterMelon = new Product()
        {
            ProductId = 2,
            ProductName = "Red Bull Watermelon",
            ProductDescription = "Energy drink with watermelon",
            Price = 3,
            StockQuantity = 20,
            Category = "Drinks",
            SupplierId = 3
        };
        Product RbGrapefruit = new Product()
        {
            ProductId = 3,
            ProductName = "Red Bull Grapefruit",
            ProductDescription = "Energy drink with grapefruit",
            Price = 4,
            StockQuantity = 15,
            Category = "Drinks",
            SupplierId = 3
        };

        modelBuilder.Entity<Product>().HasData(RbWodka, RbWaterMelon, RbGrapefruit);

        Product G603 = new Product()
        {
            ProductId = 4,
            ProductName = "Logitech G603",
            ProductDescription = "Wireless gaming mouse",
            Price = 70,
            StockQuantity = 5,
            Category = "Electronics",
            SupplierId = 1
        };
        Product GPro = new Product()
        {
            ProductId = 5,
            ProductName = "Logitech G Pro",
            ProductDescription = "Wired gaming mouse",
            Price = 50,
            StockQuantity = 10,
            Category = "Electronics",
            SupplierId = 1
        };
        Product G213 = new Product()
        {
            ProductId = 6,
            ProductName = "Logitech G213",
            ProductDescription = "Gaming keyboard",
            Price = 40,
            StockQuantity = 15,
            Category = "Electronics",
            SupplierId = 1
        };

        modelBuilder.Entity<Product>().HasData(G603, GPro, G213);

        Product pikachu = new Product()
        {
            ProductId = 7,
            ProductName = "Pikachu",
            ProductDescription = "Electric pokemon",
            Price = 100,
            StockQuantity = 1,
            Category = "Toys",
            SupplierId = 2
        };

        Product snorlax = new Product()
        {
            ProductId = 8,
            ProductName = "Snorlax",
            ProductDescription = "Sleeping pokemon",
            Price = 200,
            StockQuantity = 1,
            Category = "Toys",
            SupplierId = 2
        };

        Product charizard = new Product()
        {
            ProductId = 9,
            ProductName = "Charizard",
            ProductDescription = "Fire pokemon",
            Price = 150,
            StockQuantity = 1,
            Category = "Toys",
            SupplierId = 2
        };

        modelBuilder.Entity<Product>().HasData(pikachu, snorlax, charizard);
    }
}
