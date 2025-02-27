﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace RouteCalculator.Data.Migrations
{
    public partial class AddNamecolumntologcentertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LogisticsCenters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "LogisticsCenters");
        }
    }
}
