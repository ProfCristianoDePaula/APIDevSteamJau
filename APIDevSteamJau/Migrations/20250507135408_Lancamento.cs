using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIDevSteamJau.Migrations
{
    /// <inheritdoc />
    public partial class Lancamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Lancamento",
                table: "Jogos",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lancamento",
                table: "Jogos");
        }
    }
}
