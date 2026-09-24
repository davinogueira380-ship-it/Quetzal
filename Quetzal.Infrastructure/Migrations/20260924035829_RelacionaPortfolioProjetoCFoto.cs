using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quetzal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelacionaPortfolioProjetoCFoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjetoCFotoId",
                table: "Portfolio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_ProjetoCFotoId",
                table: "Portfolio",
                column: "ProjetoCFotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_ProjetoCFotos_ProjetoCFotoId",
                table: "Portfolio",
                column: "ProjetoCFotoId",
                principalTable: "ProjetoCFotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_ProjetoCFotos_ProjetoCFotoId",
                table: "Portfolio");

            migrationBuilder.DropIndex(
                name: "IX_Portfolio_ProjetoCFotoId",
                table: "Portfolio");

            migrationBuilder.DropColumn(
                name: "ProjetoCFotoId",
                table: "Portfolio");
        }
    }
}
