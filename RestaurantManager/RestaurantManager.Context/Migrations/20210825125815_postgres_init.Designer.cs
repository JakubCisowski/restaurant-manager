﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RestaurantManager.Context;

namespace RestaurantManager.SqlContext.Migrations
{
    [DbContext(typeof(RestaurantDbContext))]
    [Migration("20210825125815_postgres_init")]
    partial class postgres_init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DishIngredient", b =>
                {
                    b.Property<Guid>("DishesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IngredientsId")
                        .HasColumnType("uuid");

                    b.HasKey("DishesId", "IngredientsId");

                    b.HasIndex("IngredientsId");

                    b.ToTable("DishIngredient");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Orders.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Orders.DishExtraIngredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("OrderItemId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderItemId");

                    b.ToTable("DishExtraIngredients");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<int>("OrderNo")
                        .HasColumnType("integer");

                    b.Property<int>("PaymentType")
                        .HasColumnType("integer");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Orders.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DishComment")
                        .HasColumnType("text");

                    b.Property<string>("DishName")
                        .HasColumnType("text");

                    b.Property<decimal>("DishPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("OrderNo")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Orders.OrderNumber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("InUsageFrom")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("OrderNo")
                        .HasColumnType("integer");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("OrderNumbers");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Orders.ShippingAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address1")
                        .HasColumnType("text");

                    b.Property<string>("Address2")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("ZipPostalCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("ShippingAddresses");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Restaurants.Dish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("BasePrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<Guid>("MenuId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Restaurants.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Restaurants.Menu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId")
                        .IsUnique();

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Restaurants.Restaurant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("DishIngredient", b =>
                {
                    b.HasOne("RestaurantManager.Entities.Restaurants.Dish", null)
                        .WithMany()
                        .HasForeignKey("DishesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantManager.Entities.Restaurants.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RestaurantManager.Entities.Orders.DishExtraIngredient", b =>
                {
                    b.HasOne("RestaurantManager.Entities.Orders.OrderItem", "OrderItem")
                        .WithMany("DishExtraIngredients")
                        .HasForeignKey("OrderItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Orders.Order", b =>
                {
                    b.HasOne("RestaurantManager.Entities.Orders.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Orders.OrderItem", b =>
                {
                    b.HasOne("RestaurantManager.Entities.Orders.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Orders.ShippingAddress", b =>
                {
                    b.HasOne("RestaurantManager.Entities.Orders.Order", "Order")
                        .WithOne("ShippingAddress")
                        .HasForeignKey("RestaurantManager.Entities.Orders.ShippingAddress", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Restaurants.Dish", b =>
                {
                    b.HasOne("RestaurantManager.Entities.Restaurants.Menu", "Menu")
                        .WithMany("Dishes")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Restaurants.Menu", b =>
                {
                    b.HasOne("RestaurantManager.Entities.Restaurants.Restaurant", "Restaurant")
                        .WithOne("Menu")
                        .HasForeignKey("RestaurantManager.Entities.Restaurants.Menu", "RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Orders.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Orders.Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("ShippingAddress");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Orders.OrderItem", b =>
                {
                    b.Navigation("DishExtraIngredients");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Restaurants.Menu", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("RestaurantManager.Entities.Restaurants.Restaurant", b =>
                {
                    b.Navigation("Menu");
                });
#pragma warning restore 612, 618
        }
    }
}
