using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hangman.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerID);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecretWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Turns = table.Column<int>(type: "int", nullable: false),
                    TriesLeft = table.Column<int>(type: "int", nullable: false),
                    AllGuessedLetters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectGuessedLetters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WrongGuessedLetters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    Won = table.Column<bool>(type: "bit", nullable: false),
                    Time = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_Games_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerID",
                table: "Games",
                column: "PlayerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
