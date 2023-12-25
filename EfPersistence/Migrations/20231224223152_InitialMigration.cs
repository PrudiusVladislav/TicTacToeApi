using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfPersistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlayerSymbol = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstPlayerId = table.Column<int>(type: "int", nullable: false),
                    SecondPlayerId = table.Column<int>(type: "int", nullable: false),
                    WinnerPlayerId = table.Column<int>(type: "int", nullable: true),
                    IsDraw = table.Column<bool>(type: "bit", nullable: false),
                    MatchDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchResults_Players_FirstPlayerId",
                        column: x => x.FirstPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchResults_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatchResults_Players_SecondPlayerId",
                        column: x => x.SecondPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchResults_Players_WinnerPlayerId",
                        column: x => x.WinnerPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchResults_FirstPlayerId",
                table: "MatchResults",
                column: "FirstPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchResults_PlayerId",
                table: "MatchResults",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchResults_SecondPlayerId",
                table: "MatchResults",
                column: "SecondPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchResults_WinnerPlayerId",
                table: "MatchResults",
                column: "WinnerPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Name",
                table: "Players",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchResults");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
