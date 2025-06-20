using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIGenerator.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPromptColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userPrompt",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userPrompt",
                table: "Resumes");
        }
    }
}
