using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIGenerator.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProviderNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "providerName",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "providerName",
                table: "Certificates");
        }
    }
}
