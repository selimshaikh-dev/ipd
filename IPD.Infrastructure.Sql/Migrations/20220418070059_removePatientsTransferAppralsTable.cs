using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class removePatientsTransferAppralsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientTransferApparatuses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientTransferApparatuses",
                columns: table => new
                {
                    PatientTransferApparatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocalReferralID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApparatusID = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientTransferApparatuses", x => x.PatientTransferApparatusId);
                    table.ForeignKey(
                        name: "FK_PatientTransferApparatuses_LocalReferrals_LocalReferralID",
                        column: x => x.LocalReferralID,
                        principalTable: "LocalReferrals",
                        principalColumn: "LocalReferralID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientTransferApparatuses_LocalReferralID",
                table: "PatientTransferApparatuses",
                column: "LocalReferralID");
        }
    }
}
