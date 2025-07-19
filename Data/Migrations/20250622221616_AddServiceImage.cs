using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIGenerator.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServiceImage",
                table: "Services",
                newName: "serviceImageName");

            migrationBuilder.AddColumn<string>(
                name: "serviceImageContentType",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "serviceImageFile",
                table: "Services",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "serviceImageContentType",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "serviceImageFile",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "serviceImageName",
                table: "Services",
                newName: "ServiceImage");
        }
    }
}
