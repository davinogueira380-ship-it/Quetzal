using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quetzal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelacionaPortfolioProjetoC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjetoCId",
                table: "Portfolio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_ProjetoCId",
                table: "Portfolio",
                column: "ProjetoCId");

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_ProjetosC_ProjetoCId",
                table: "Portfolio",
                column: "ProjetoCId",
                principalTable: "ProjetosC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_ProjetosC_ProjetoCId",
                table: "Portfolio");

            migrationBuilder.DropIndex(
                name: "IX_Portfolio_ProjetoCId",
                table: "Portfolio");

            migrationBuilder.DropColumn(
                name: "ProjetoCId",
                table: "Portfolio");
        }
    }
}
