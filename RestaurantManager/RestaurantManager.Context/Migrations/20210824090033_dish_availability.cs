using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantManager.SqlContext.Migrations
{
    public partial class dish_availability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Dishes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Dishes");
        }
    }
}
