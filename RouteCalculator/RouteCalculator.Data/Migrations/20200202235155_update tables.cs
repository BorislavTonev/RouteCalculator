using Microsoft.EntityFrameworkCore.Migrations;

namespace RouteCalculator.Data.Migrations
{
    public partial class updatetables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "Roads",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "EndingPoint",
                table: "Roads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Roads",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartingPoint",
                table: "Roads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Locations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Roads");

            migrationBuilder.DropColumn(
                name: "EndingPoint",
                table: "Roads");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Roads");

            migrationBuilder.DropColumn(
                name: "StartingPoint",
                table: "Roads");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Locations");
        }
    }
}
