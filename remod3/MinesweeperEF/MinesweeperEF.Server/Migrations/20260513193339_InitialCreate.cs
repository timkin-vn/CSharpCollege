using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinesweeperEF.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SavedGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Rows = table.Column<int>(type: "INTEGER", nullable: false),
                    Cols = table.Column<int>(type: "INTEGER", nullable: false),
                    Mines = table.Column<int>(type: "INTEGER", nullable: false),
                    FlagsLeft = table.Column<int>(type: "INTEGER", nullable: false),
                    HasStarted = table.Column<bool>(type: "INTEGER", nullable: false),
                    GameOver = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasWon = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedGames_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameCells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SavedGameId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Row = table.Column<int>(type: "INTEGER", nullable: false),
                    Col = table.Column<int>(type: "INTEGER", nullable: false),
                    IsMine = table.Column<bool>(type: "INTEGER", nullable: false),
                    AdjacentMines = table.Column<int>(type: "INTEGER", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameCells_SavedGames_SavedGameId",
                        column: x => x.SavedGameId,
                        principalTable: "SavedGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameCells_SavedGameId",
                table: "GameCells",
                column: "SavedGameId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedGames_UserId",
                table: "SavedGames",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCells");

            migrationBuilder.DropTable(
                name: "SavedGames");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
