using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantManager.Context.Migrations
{
    public partial class Relations_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Dishes_DishId",
                table: "OrderItem");

            migrationBuilder.DropTable(
                name: "IngredientOrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_DishId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "DishId",
                table: "OrderItem");

            migrationBuilder.AddColumn<string>(
                name: "DishName",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DishPrice",
                table: "OrderItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "DishExtraIngredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishExtraIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DishExtraIngredients_OrderItem_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishExtraIngredients_OrderItemId",
                table: "DishExtraIngredients",
                column: "OrderItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishExtraIngredients");

            migrationBuilder.DropColumn(
                name: "DishName",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "DishPrice",
                table: "OrderItem");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                table: "Restaurants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DishId",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "IngredientOrderItem",
                columns: table => new
                {
                    IngredientsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderItemsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientOrderItem", x => new { x.IngredientsId, x.OrderItemsId });
                    table.ForeignKey(
                        name: "FK_IngredientOrderItem_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
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
                name: "IX_OrderItem_DishId",
                table: "OrderItem",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientOrderItem_OrderItemsId",
                table: "IngredientOrderItem",
                column: "OrderItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Dishes_DishId",
                table: "OrderItem",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
