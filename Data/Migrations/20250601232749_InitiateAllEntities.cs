using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIGenerator.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitiateAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "fristName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    portfolioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    portfolioImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ModifiedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    secondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    thirdName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    linkedInLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    githubLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    facebookLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.portfolioId);
                    table.ForeignKey(
                        name: "FK_Portfolios_AspNetUsers_endUserId",
                        column: x => x.endUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    resumeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ModifiedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    secondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    thirdName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    linkedInLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    githubLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    facebookLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.resumeId);
                    table.ForeignKey(
                        name: "FK_Resumes_AspNetUsers_endUserId",
                        column: x => x.endUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    serviceId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    portfolioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.serviceId);
                    table.ForeignKey(
                        name: "FK_Services_Portfolios_portfolioId",
                        column: x => x.portfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "portfolioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    certificateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endDate = table.Column<DateOnly>(type: "date", nullable: false),
                    topicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GPA = table.Column<double>(type: "float", nullable: true),
                    resumeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.certificateId);
                    table.ForeignKey(
                        name: "FK_Certificates_Resumes_resumeId",
                        column: x => x.resumeId,
                        principalTable: "Resumes",
                        principalColumn: "resumeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    educationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    collegeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    degreeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endDate = table.Column<DateOnly>(type: "date", nullable: false),
                    majorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GPA = table.Column<double>(type: "float", nullable: true),
                    resumeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.educationId);
                    table.ForeignKey(
                        name: "FK_Educations_Resumes_resumeId",
                        column: x => x.resumeId,
                        principalTable: "Resumes",
                        principalColumn: "resumeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    experienceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ednDate = table.Column<DateOnly>(type: "date", nullable: false),
                    isCurrent = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    duties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resumeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.experienceId);
                    table.ForeignKey(
                        name: "FK_Experiences_Resumes_resumeId",
                        column: x => x.resumeId,
                        principalTable: "Resumes",
                        principalColumn: "resumeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    languageId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    languageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    level = table.Column<short>(type: "smallint", nullable: false),
                    resumeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.languageId);
                    table.ForeignKey(
                        name: "FK_Languages_Resumes_resumeId",
                        column: x => x.resumeId,
                        principalTable: "Resumes",
                        principalColumn: "resumeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    skillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    skillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    skillType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resumeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.skillId);
                    table.ForeignKey(
                        name: "FK_Skills_Resumes_resumeId",
                        column: x => x.resumeId,
                        principalTable: "Resumes",
                        principalColumn: "resumeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    projectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    projectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endDate = table.Column<DateOnly>(type: "date", nullable: false),
                    serviceId = table.Column<short>(type: "smallint", nullable: false),
                    portfolioId = table.Column<int>(type: "int", nullable: false),
                    projectLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.projectId);
                    table.ForeignKey(
                        name: "FK_Projects_Portfolios_portfolioId",
                        column: x => x.portfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "portfolioId");
                    table.ForeignKey(
                        name: "FK_Projects_Services_serviceId",
                        column: x => x.serviceId,
                        principalTable: "Services",
                        principalColumn: "serviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_resumeId",
                table: "Certificates",
                column: "resumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_resumeId",
                table: "Educations",
                column: "resumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_resumeId",
                table: "Experiences",
                column: "resumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_resumeId",
                table: "Languages",
                column: "resumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_endUserId",
                table: "Portfolios",
                column: "endUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_portfolioId",
                table: "Projects",
                column: "portfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_serviceId",
                table: "Projects",
                column: "serviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_endUserId",
                table: "Resumes",
                column: "endUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_portfolioId",
                table: "Services",
                column: "portfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_resumeId",
                table: "Skills",
                column: "resumeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Resumes");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "fristName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "lastName",
                table: "AspNetUsers");
        }
    }
}
