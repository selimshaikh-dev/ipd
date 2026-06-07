using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class CreateLiquirAndMouldingEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Liquor_Partograph_PartographID",
                table: "Liquor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Liquor",
                table: "Liquor");

            migrationBuilder.RenameTable(
                name: "Liquor",
                newName: "Liquors");

            migrationBuilder.RenameIndex(
                name: "IX_Liquor_PartographID",
                table: "Liquors",
                newName: "IX_Liquors_PartographID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Liquors",
                table: "Liquors",
                column: "LiquorID");

            migrationBuilder.CreateTable(
                name: "Mouldings",
                columns: table => new
                {
                    MouldingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MouldingDetails = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MouldingTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
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
                    table.PrimaryKey("PK_Mouldings", x => x.MouldingID);
                    table.ForeignKey(
                        name: "FK_Mouldings_Partograph_PartographID",
                        column: x => x.PartographID,
                        principalTable: "Partograph",
                        principalColumn: "PartographID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mouldings_PartographID",
                table: "Mouldings",
                column: "PartographID");

            migrationBuilder.AddForeignKey(
                name: "FK_Liquors_Partograph_PartographID",
                table: "Liquors",
                column: "PartographID",
                principalTable: "Partograph",
                principalColumn: "PartographID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Liquors_Partograph_PartographID",
                table: "Liquors");

            migrationBuilder.DropTable(
                name: "Mouldings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Liquors",
                table: "Liquors");

            migrationBuilder.RenameTable(
                name: "Liquors",
                newName: "Liquor");

            migrationBuilder.RenameIndex(
                name: "IX_Liquors_PartographID",
                table: "Liquor",
                newName: "IX_Liquor_PartographID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Liquor",
                table: "Liquor",
                column: "LiquorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Liquor_Partograph_PartographID",
                table: "Liquor",
                column: "PartographID",
                principalTable: "Partograph",
                principalColumn: "PartographID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
