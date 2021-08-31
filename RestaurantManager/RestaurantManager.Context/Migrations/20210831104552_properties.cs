using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantManager.SqlContext.Migrations
{
    public partial class properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_ShippingOptions_ShippingOptionsId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_ShippingOptionsId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "ShippingOptionsId",
                table: "Restaurants");

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantId",
                table: "ShippingOptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ShippingOptions_RestaurantId",
                table: "ShippingOptions",
                column: "RestaurantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOptions_Restaurants_RestaurantId",
                table: "ShippingOptions",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOptions_Restaurants_RestaurantId",
                table: "ShippingOptions");

            migrationBuilder.DropIndex(
                name: "IX_ShippingOptions_RestaurantId",
                table: "ShippingOptions");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "ShippingOptions");

            migrationBuilder.AddColumn<Guid>(
                name: "ShippingOptionsId",
                table: "Restaurants",
                type: "uuid",
                nullable: true);

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
    }
}
