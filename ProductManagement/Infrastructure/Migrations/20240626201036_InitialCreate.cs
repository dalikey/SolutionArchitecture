using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductDescription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Category", "Price", "ProductDescription", "ProductName", "StockQuantity", "SupplierId" },
                values: new object[,]
                {
                    { 1, "Drinks", 5, "Energy drink with wodka", "Red Bull Wodka", 10, 3 },
                    { 2, "Drinks", 3, "Energy drink with watermelon", "Red Bull Watermelon", 20, 3 },
                    { 3, "Drinks", 4, "Energy drink with grapefruit", "Red Bull Grapefruit", 15, 3 },
                    { 4, "Electronics", 70, "Wireless gaming mouse", "Logitech G603", 5, 1 },
                    { 5, "Electronics", 50, "Wired gaming mouse", "Logitech G Pro", 10, 1 },
                    { 6, "Electronics", 40, "Gaming keyboard", "Logitech G213", 15, 1 },
                    { 7, "Toys", 100, "Electric pokemon", "Pikachu", 1, 2 },
                    { 8, "Toys", 200, "Sleeping pokemon", "Snorlax", 1, 2 },
                    { 9, "Toys", 150, "Fire pokemon", "Charizard", 1, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
