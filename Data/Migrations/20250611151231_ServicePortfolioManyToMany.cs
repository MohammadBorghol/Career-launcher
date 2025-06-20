using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIGenerator.Data.Migrations
{
    /// <inheritdoc />
    public partial class ServicePortfolioManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Portfolios_portfolioId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_portfolioId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "portfolioId",
                table: "Services");

            migrationBuilder.CreateTable(
                name: "portfolioServices",
                columns: table => new
                {
                    serviceId = table.Column<short>(type: "smallint", nullable: false),
                    portfolioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_portfolioServices", x => new { x.portfolioId, x.serviceId });
                    table.ForeignKey(
                        name: "FK_portfolioServices_Portfolios_portfolioId",
                        column: x => x.portfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "portfolioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_portfolioServices_Services_serviceId",
                        column: x => x.serviceId,
                        principalTable: "Services",
                        principalColumn: "serviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_portfolioServices_serviceId",
                table: "portfolioServices",
                column: "serviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "portfolioServices");

            migrationBuilder.AddColumn<int>(
                name: "portfolioId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Services_portfolioId",
                table: "Services",
                column: "portfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Portfolios_portfolioId",
                table: "Services",
                column: "portfolioId",
                principalTable: "Portfolios",
                principalColumn: "portfolioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
