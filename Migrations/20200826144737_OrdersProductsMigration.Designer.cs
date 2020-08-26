﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eCommerceRestAPI.Models;

namespace eCommerceRestAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200826144737_OrdersProductsMigration")]
    partial class OrdersProductsMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eCommerceRestAPI.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("eCommerceRestAPI.Models.OrderProduct", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("eCommerceRestAPI.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImageUrl = "https://st.depositphotos.com/2251265/4803/i/450/depositphotos_48037605-stock-photo-man-wearing-t-shirt.jpg",
                            Name = "Rubberised Print T-Shirt",
                            Price = 9.99m
                        },
                        new
                        {
                            Id = 2,
                            ImageUrl = "https://picture-cdn.wheretoget.it/tvrznj-i.jpg",
                            Name = "Contrast Top TRF",
                            Price = 11.99m
                        },
                        new
                        {
                            Id = 3,
                            ImageUrl = "https://celticandco.global.ssl.fastly.net/usercontent/img/col-12/69602.jpg",
                            Name = "Tied Leather Heeled Sandals",
                            Price = 49.95m
                        },
                        new
                        {
                            Id = 4,
                            ImageUrl = "https://cf.shopee.com.my/file/36df2e1d04ca103f16ccefffa9927728",
                            Name = "Leather High Heel Sandals With Gathering",
                            Price = 39.95m
                        },
                        new
                        {
                            Id = 5,
                            ImageUrl = "https://cf.shopee.ph/file/fecc650ca5802d709890a66cc00cfe23",
                            Name = "Pleated Palazzo Trousers TRF",
                            Price = 29.95m
                        },
                        new
                        {
                            Id = 6,
                            ImageUrl = "https://emma.bg/images/products/damski-pantalon-faded-black-super-skinny-trousers-1.jpg",
                            Name = "Skinny Trousers With Belt",
                            Price = 19.99m
                        });
                });

            modelBuilder.Entity("eCommerceRestAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CurrencyCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("eCommerceRestAPI.Models.Order", b =>
                {
                    b.HasOne("eCommerceRestAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eCommerceRestAPI.Models.OrderProduct", b =>
                {
                    b.HasOne("eCommerceRestAPI.Models.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eCommerceRestAPI.Models.Product", "Product")
                        .WithMany("Orders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
