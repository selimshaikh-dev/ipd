using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class createTableDecentOfHead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cervix_Partograph_PartographID",
                table: "Cervix");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cervix",
                table: "Cervix");

            migrationBuilder.RenameTable(
                name: "Cervix",
                newName: "Cervixes");

            migrationBuilder.RenameIndex(
                name: "IX_Cervix_PartographID",
                table: "Cervixes",
                newName: "IX_Cervixes_PartographID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cervixes",
                table: "Cervixes",
                column: "CervixID");

            migrationBuilder.CreateTable(
                name: "DescentOfHeads",
                columns: table => new
                {
                    DescentOfHeadID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DescentOfHeadDetails = table.Column<int>(type: "int", maxLength: 30, nullable: false),
                    DescentOfHeadTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
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
                    table.PrimaryKey("PK_DescentOfHeads", x => x.DescentOfHeadID);
                    table.ForeignKey(
                        name: "FK_DescentOfHeads_Partograph_PartographID",
                        column: x => x.PartographID,
                        principalTable: "Partograph",
                        principalColumn: "PartographID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DescentOfHeads_PartographID",
                table: "DescentOfHeads",
                column: "PartographID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cervixes_Partograph_PartographID",
                table: "Cervixes",
                column: "PartographID",
                principalTable: "Partograph",
                principalColumn: "PartographID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cervixes_Partograph_PartographID",
                table: "Cervixes");

            migrationBuilder.DropTable(
                name: "DescentOfHeads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cervixes",
                table: "Cervixes");

            migrationBuilder.RenameTable(
                name: "Cervixes",
                newName: "Cervix");

            migrationBuilder.RenameIndex(
                name: "IX_Cervixes_PartographID",
                table: "Cervix",
                newName: "IX_Cervix_PartographID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cervix",
                table: "Cervix",
                column: "CervixID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cervix_Partograph_PartographID",
                table: "Cervix",
                column: "PartographID",
                principalTable: "Partograph",
                principalColumn: "PartographID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
