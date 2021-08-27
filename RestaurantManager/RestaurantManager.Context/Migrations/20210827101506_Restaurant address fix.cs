using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantManager.SqlContext.Migrations
{
    public partial class Restaurantaddressfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantAddress_Restaurants_RestaurantId",
                table: "RestaurantAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantAddress",
                table: "RestaurantAddress");

            migrationBuilder.RenameTable(
                name: "RestaurantAddress",
                newName: "RestaurantAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantAddress_RestaurantId",
                table: "RestaurantAddresses",
                newName: "IX_RestaurantAddresses_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantAddresses",
                table: "RestaurantAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantAddresses_Restaurants_RestaurantId",
                table: "RestaurantAddresses",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantAddresses_Restaurants_RestaurantId",
                table: "RestaurantAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantAddresses",
                table: "RestaurantAddresses");

            migrationBuilder.RenameTable(
                name: "RestaurantAddresses",
                newName: "RestaurantAddress");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantAddresses_RestaurantId",
                table: "RestaurantAddress",
                newName: "IX_RestaurantAddress_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantAddress",
                table: "RestaurantAddress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantAddress_Restaurants_RestaurantId",
                table: "RestaurantAddress",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
