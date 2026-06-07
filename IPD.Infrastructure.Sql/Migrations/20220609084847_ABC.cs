using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class ABC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalReferrals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalReferrals",
                columns: table => new
                {
                    LocalReferralID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcedureID = table.Column<int>(type: "int", nullable: false),
                    CivilServent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactDetails = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Discipline = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmploymentNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Investigation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PatientsTransferApparatus = table.Column<byte>(type: "tinyint", nullable: false),
                    Phalala = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PracticeNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReasonReferral = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReferralType = table.Column<byte>(type: "tinyint", nullable: false),
                    ReferringSpecialist = table.Column<string>(type: "nvarchar(92)", maxLength: 92, nullable: false),
                    ShortHistory = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Time = table.Column<DateTime>(type: "SmallDateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalReferrals", x => x.LocalReferralID);
                    table.ForeignKey(
                        name: "FK_LocalReferrals_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocalReferrals_Procedure_ProcedureID",
                        column: x => x.ProcedureID,
                        principalTable: "Procedure",
                        principalColumn: "ProcedureID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalReferrals_AdmissionID",
                table: "LocalReferrals",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_LocalReferrals_ProcedureID",
                table: "LocalReferrals",
                column: "ProcedureID");
        }
    }
}
