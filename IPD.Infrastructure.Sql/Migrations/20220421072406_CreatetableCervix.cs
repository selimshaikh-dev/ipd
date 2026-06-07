using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class CreatetableCervix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cervix",
                columns: table => new
                {
                    CervixID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CervixDetails = table.Column<int>(type: "int", maxLength: 30, nullable: false),
                    CervixTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    PartographID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cervix", x => x.CervixID);
                    table.ForeignKey(
                        name: "FK_Cervix_Partograph_PartographID",
                        column: x => x.PartographID,
                        principalTable: "Partograph",
                        principalColumn: "PartographID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cervix_PartographID",
                table: "Cervix",
                column: "PartographID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cervix");
        }
    }
}
