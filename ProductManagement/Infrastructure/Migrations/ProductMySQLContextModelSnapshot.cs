﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductManagement.Infrastructure;

#nullable disable

namespace ProductManagement.Infrastructure.Migrations
{
    [DbContext(typeof(ProductMySQLContext))]
    partial class ProductMySQLContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ProductManagement.Domain.Product", b =>
                {
                    b.Property<int?>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int?>("ProductId"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Category = "Drinks",
                            Price = 5,
                            ProductDescription = "Energy drink with wodka",
                            ProductName = "Red Bull Wodka",
                            StockQuantity = 10,
                            SupplierId = 3
                        },
                        new
                        {
                            ProductId = 2,
                            Category = "Drinks",
                            Price = 3,
                            ProductDescription = "Energy drink with watermelon",
                            ProductName = "Red Bull Watermelon",
                            StockQuantity = 20,
                            SupplierId = 3
                        },
                        new
                        {
                            ProductId = 3,
                            Category = "Drinks",
                            Price = 4,
                            ProductDescription = "Energy drink with grapefruit",
                            ProductName = "Red Bull Grapefruit",
                            StockQuantity = 15,
                            SupplierId = 3
                        },
                        new
                        {
                            ProductId = 4,
                            Category = "Electronics",
                            Price = 70,
                            ProductDescription = "Wireless gaming mouse",
                            ProductName = "Logitech G603",
                            StockQuantity = 5,
                            SupplierId = 1
                        },
                        new
                        {
                            ProductId = 5,
                            Category = "Electronics",
                            Price = 50,
                            ProductDescription = "Wired gaming mouse",
                            ProductName = "Logitech G Pro",
                            StockQuantity = 10,
                            SupplierId = 1
                        },
                        new
                        {
                            ProductId = 6,
                            Category = "Electronics",
                            Price = 40,
                            ProductDescription = "Gaming keyboard",
                            ProductName = "Logitech G213",
                            StockQuantity = 15,
                            SupplierId = 1
                        },
                        new
                        {
                            ProductId = 7,
                            Category = "Toys",
                            Price = 100,
                            ProductDescription = "Electric pokemon",
                            ProductName = "Pikachu",
                            StockQuantity = 1,
                            SupplierId = 2
                        },
                        new
                        {
                            ProductId = 8,
                            Category = "Toys",
                            Price = 200,
                            ProductDescription = "Sleeping pokemon",
                            ProductName = "Snorlax",
                            StockQuantity = 1,
                            SupplierId = 2
                        },
                        new
                        {
                            ProductId = 9,
                            Category = "Toys",
                            Price = 150,
                            ProductDescription = "Fire pokemon",
                            ProductName = "Charizard",
                            StockQuantity = 1,
                            SupplierId = 2
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
