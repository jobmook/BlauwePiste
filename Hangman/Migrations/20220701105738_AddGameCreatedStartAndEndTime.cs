using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hangman.Migrations
{
    public partial class AddGameCreatedStartAndEndTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "EndTime",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartTime",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Games");

            migrationBuilder.AddColumn<long>(
                name: "Time",
                table: "Games",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
