using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantManager.Context.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientOrderItem_Ingredients_ExtraIngredientsId",
                table: "IngredientOrderItem");

            migrationBuilder.RenameColumn(
                name: "ExtraIngredientsId",
                table: "IngredientOrderItem",
                newName: "IngredientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientOrderItem_Ingredients_IngredientsId",
                table: "IngredientOrderItem",
                column: "IngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientOrderItem_Ingredients_IngredientsId",
                table: "IngredientOrderItem");

            migrationBuilder.RenameColumn(
                name: "IngredientsId",
                table: "IngredientOrderItem",
                newName: "ExtraIngredientsId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientOrderItem_Ingredients_ExtraIngredientsId",
                table: "IngredientOrderItem",
                column: "ExtraIngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
