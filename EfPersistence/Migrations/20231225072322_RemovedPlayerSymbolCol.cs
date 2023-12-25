using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfPersistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPlayerSymbolCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerSymbol",
                table: "Players");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayerSymbol",
                table: "Players",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }
    }
}
