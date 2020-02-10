using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RouteCalculator.Data.Migrations
{
    public partial class Adddatecolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CalculatonDate",
                table: "Roads",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RoadId",
                table: "LogisticsCenters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CalculatonDate",
                table: "Locations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculatonDate",
                table: "Roads");

            migrationBuilder.DropColumn(
                name: "RoadId",
                table: "LogisticsCenters");

            migrationBuilder.DropColumn(
                name: "CalculatonDate",
                table: "Locations");
        }
    }
}
