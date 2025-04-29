using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIDevSteamJau.Migrations
{
    /// <inheritdoc />
    public partial class cupomcarrinho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LimiteUso",
                table: "Cupons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CuponsCarrinho",
                columns: table => new
                {
                    CupomCarrinhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarrinhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CupomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAplicacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuponsCarrinho", x => x.CupomCarrinhoId);
                    table.ForeignKey(
                        name: "FK_CuponsCarrinho_Carrinhos_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Carrinhos",
                        principalColumn: "CarrinhoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuponsCarrinho_Cupons_CupomId",
                        column: x => x.CupomId,
                        principalTable: "Cupons",
                        principalColumn: "CupomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuponsCarrinho_CarrinhoId",
                table: "CuponsCarrinho",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_CuponsCarrinho_CupomId",
                table: "CuponsCarrinho",
                column: "CupomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuponsCarrinho");

            migrationBuilder.DropColumn(
                name: "LimiteUso",
                table: "Cupons");
        }
    }
}
