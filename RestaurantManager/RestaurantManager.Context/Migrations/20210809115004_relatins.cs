using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RestaurantManager.Context.Migrations
{
    public partial class relatins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_OrderItem_OrderItemId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_OrderItemId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "IngredientOrderItem",
                columns: table => new
                {
                    ExtraIngredientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderItemsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientOrderItem", x => new { x.ExtraIngredientsId, x.OrderItemsId });
                    table.ForeignKey(
                        name: "FK_IngredientOrderItem_Ingredients_ExtraIngredientsId",
                        column: x => x.ExtraIngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientOrderItem_OrderItem_OrderItemsId",
                        column: x => x.OrderItemsId,
                        principalTable: "OrderItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientOrderItem_OrderItemsId",
                table: "IngredientOrderItem",
                column: "OrderItemsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientOrderItem");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderItemId",
                table: "Ingredients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_OrderItemId",
                table: "Ingredients",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_OrderItem_OrderItemId",
                table: "Ingredients",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
