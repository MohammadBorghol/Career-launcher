using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIGenerator.Data.Migrations
{
    /// <inheritdoc />
    public partial class PortfolioAndProjectImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "portfolioImage",
                table: "Portfolios",
                newName: "portfolioImageName");

            migrationBuilder.AddColumn<string>(
                name: "projectImageContentType",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "projectImageFile",
                table: "Projects",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "projectImageName",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "portfolioImageContentType",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "portfolioImageFile",
                table: "Portfolios",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "projectImageContentType",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "projectImageFile",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "projectImageName",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "portfolioImageContentType",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "portfolioImageFile",
                table: "Portfolios");

            migrationBuilder.RenameColumn(
                name: "portfolioImageName",
                table: "Portfolios",
                newName: "portfolioImage");
        }
    }
}
