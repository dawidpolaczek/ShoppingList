using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingList.Migrations
{
    public partial class BasketUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DayOfWeek",
                table: "Baskets",
                newName: "DayEveryWeek");

            migrationBuilder.AddColumn<DateTime>(
                name: "SpecificDate",
                table: "Baskets",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecificDate",
                table: "Baskets");

            migrationBuilder.RenameColumn(
                name: "DayEveryWeek",
                table: "Baskets",
                newName: "DayOfWeek");
        }
    }
}
