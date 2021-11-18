using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class visitors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitorEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisitorName = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    VistorAddress = table.Column<string>(type: "TEXT", nullable: true),
                    IdType = table.Column<string>(type: "TEXT", nullable: true),
                    IdProofNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Devices = table.Column<string>(type: "TEXT", nullable: true),
                    PurposeOfVisit = table.Column<string>(type: "TEXT", nullable: true),
                    PersonToMeet = table.Column<string>(type: "TEXT", nullable: true),
                    InTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OutTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ImageFilename = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorEntries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitorEntries");
        }
    }
}
