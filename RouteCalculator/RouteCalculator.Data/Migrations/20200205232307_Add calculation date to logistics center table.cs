using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RouteCalculator.Data.Migrations
{
    public partial class Addcalculationdatetologisticscentertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CalculationDate",
                table: "LogisticsCenters",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculationDate",
                table: "LogisticsCenters");
        }
    }
}
