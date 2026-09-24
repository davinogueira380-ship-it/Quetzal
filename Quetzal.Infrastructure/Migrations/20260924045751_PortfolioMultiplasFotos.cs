using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quetzal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PortfolioMultiplasFotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "PortfolioFotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioId = table.Column<int>(type: "int", nullable: false),
                    ProjetoCFotoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioFotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioFotos_Portfolio_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioFotos_ProjetoCFotos_ProjetoCFotoId",
                        column: x => x.ProjetoCFotoId,
                        principalTable: "ProjetoCFotos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioFotos_PortfolioId_ProjetoCFotoId",
                table: "PortfolioFotos",
                columns: new[] { "PortfolioId", "ProjetoCFotoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioFotos_ProjetoCFotoId",
                table: "PortfolioFotos",
                column: "ProjetoCFotoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioFotos");

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
    }
}
