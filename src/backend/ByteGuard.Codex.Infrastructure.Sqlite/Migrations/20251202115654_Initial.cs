using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteGuard.Codex.Infrastructure.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsvsVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    VersionNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsvsVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomRequirement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Ordinal = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomRequirement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AsvsChapters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Ordinal = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    AsvsVersionId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsvsChapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsvsChapters_AsvsVersions_AsvsVersionId",
                        column: x => x.AsvsVersionId,
                        principalTable: "AsvsVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Owner = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArchivedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AsvsVersionId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_AsvsVersions_AsvsVersionId",
                        column: x => x.AsvsVersionId,
                        principalTable: "AsvsVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AsvsSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Ordinal = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AsvsChapterId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsvsSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsvsSections_AsvsChapters_AsvsChapterId",
                        column: x => x.AsvsChapterId,
                        principalTable: "AsvsChapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AsvsRequirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AsvsSectionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Ordinal = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsvsRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsvsRequirements_AsvsSections_AsvsSectionId",
                        column: x => x.AsvsSectionId,
                        principalTable: "AsvsSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRequirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    AsvsRequirementId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CustomRequirementId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    EvidenceLink = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectRequirements_AsvsRequirements_AsvsRequirementId",
                        column: x => x.AsvsRequirementId,
                        principalTable: "AsvsRequirements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectRequirements_CustomRequirement_CustomRequirementId",
                        column: x => x.CustomRequirementId,
                        principalTable: "CustomRequirement",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectRequirements_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsvsChapters_AsvsVersionId",
                table: "AsvsChapters",
                column: "AsvsVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_AsvsRequirements_AsvsSectionId",
                table: "AsvsRequirements",
                column: "AsvsSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AsvsSections_AsvsChapterId",
                table: "AsvsSections",
                column: "AsvsChapterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequirements_AsvsRequirementId",
                table: "ProjectRequirements",
                column: "AsvsRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequirements_CustomRequirementId",
                table: "ProjectRequirements",
                column: "CustomRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequirements_ProjectId",
                table: "ProjectRequirements",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AsvsVersionId",
                table: "Projects",
                column: "AsvsVersionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectRequirements");

            migrationBuilder.DropTable(
                name: "AsvsRequirements");

            migrationBuilder.DropTable(
                name: "CustomRequirement");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "AsvsSections");

            migrationBuilder.DropTable(
                name: "AsvsChapters");

            migrationBuilder.DropTable(
                name: "AsvsVersions");
        }
    }
}
