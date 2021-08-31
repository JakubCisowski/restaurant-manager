using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantManager.SqlContext.Migrations
{
    public partial class Restaurant_Shipping_Options : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShippingOptionsId",
                table: "Restaurants",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShippingOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaxShippingDistanceRadius = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingOptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_ShippingOptionsId",
                table: "Restaurants",
                column: "ShippingOptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_ShippingOptions_ShippingOptionsId",
                table: "Restaurants",
                column: "ShippingOptionsId",
                principalTable: "ShippingOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_ShippingOptions_ShippingOptionsId",
                table: "Restaurants");

            migrationBuilder.DropTable(
                name: "ShippingOptions");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_ShippingOptionsId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "ShippingOptionsId",
                table: "Restaurants");
        }
    }
}
