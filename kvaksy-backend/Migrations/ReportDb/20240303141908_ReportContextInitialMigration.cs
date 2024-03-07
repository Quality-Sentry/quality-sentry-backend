using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kvaksy_backend.Migrations.ReportDb
{
    public partial class ReportContextInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportFieldsConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageField = table.Column<bool>(type: "bit", nullable: false),
                    TemperatureField = table.Column<bool>(type: "bit", nullable: false),
                    WeightField = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFieldsConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Finished = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportFieldBase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    Temperature = table.Column<double>(type: "float", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFieldBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportFieldBase_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImageFieldUrl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageFieldId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageFieldUrl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageFieldUrl_ReportFieldBase_ImageFieldId",
                        column: x => x.ImageFieldId,
                        principalTable: "ReportFieldBase",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageFieldUrl_ImageFieldId",
                table: "ImageFieldUrl",
                column: "ImageFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFieldBase_ReportId",
                table: "ReportFieldBase",
                column: "ReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageFieldUrl");

            migrationBuilder.DropTable(
                name: "ReportFieldsConfiguration");

            migrationBuilder.DropTable(
                name: "ReportFieldBase");

            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
