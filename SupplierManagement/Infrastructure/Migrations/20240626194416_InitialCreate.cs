using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SupplierManagement.Infrastructure.Migrations
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
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SupplierName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactEmail = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactPhone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductDescription = table.Column<string>(type: "longtext", nullable: true, defaultValue: "No description")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    StockQuantity = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Category = table.Column<string>(type: "longtext", nullable: true, defaultValue: "No category")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SupplierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierId", "Address", "ContactEmail", "ContactName", "ContactPhone", "SupplierName" },
                values: new object[,]
                {
                    { 1, "Logitech address", "Logitech@mail.com", "John Doe", "123456789", "Logitech BV." },
                    { 2, "Pokemon address", "Pokemon@mail.com", "Ash Ketchum", "987654321", "Pokemon Inc." },
                    { 3, "Red Bull Racing address", "Redbull@mail.com", "Max Verstappen", "123456789", "Red Bull Racing" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_SupplierId",
                table: "Suppliers",
                column: "SupplierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_SupplierName",
                table: "Suppliers",
                column: "SupplierName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
