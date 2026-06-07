using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class ModifiedAllEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discharges_DischargeStatuses_DischargeStatusesDischargeStatusID",
                table: "Discharges");

            migrationBuilder.DropTable(
                name: "ExaminationDetails");

            migrationBuilder.DropTable(
                name: "PatientDetails");

            migrationBuilder.DropIndex(
                name: "IX_Discharges_DischargeStatusesDischargeStatusID",
                table: "Discharges");

            migrationBuilder.DropColumn(
                name: "DischargeStatusesDischargeStatusID",
                table: "Discharges");

            migrationBuilder.RenameColumn(
                name: "DischargeStatus",
                table: "DischargeStatuses",
                newName: "DischargesStatus");

            migrationBuilder.CreateTable(
                name: "ExaminationDetail",
                columns: table => new
                {
                    ExaminationDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DigonosisExaminationID = table.Column<int>(type: "int", nullable: false),
                    PatientExaminationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationDetail", x => x.ExaminationDetailID);
                    table.ForeignKey(
                        name: "FK_ExaminationDetail_DiagonosisExaminations_DigonosisExaminationID",
                        column: x => x.DigonosisExaminationID,
                        principalTable: "DiagonosisExaminations",
                        principalColumn: "DigonosisExaminationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationDetail_PatientExaminations_PatientExaminationID",
                        column: x => x.PatientExaminationID,
                        principalTable: "PatientExaminations",
                        principalColumn: "PatientExaminationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discharges_DischargeStatusID",
                table: "Discharges",
                column: "DischargeStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDetail_DigonosisExaminationID",
                table: "ExaminationDetail",
                column: "DigonosisExaminationID");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDetail_PatientExaminationID",
                table: "ExaminationDetail",
                column: "PatientExaminationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Discharges_DischargeStatuses_DischargeStatusID",
                table: "Discharges",
                column: "DischargeStatusID",
                principalTable: "DischargeStatuses",
                principalColumn: "DischargeStatusID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discharges_DischargeStatuses_DischargeStatusID",
                table: "Discharges");

            migrationBuilder.DropTable(
                name: "ExaminationDetail");

            migrationBuilder.DropIndex(
                name: "IX_Discharges_DischargeStatusID",
                table: "Discharges");

            migrationBuilder.RenameColumn(
                name: "DischargesStatus",
                table: "DischargeStatuses",
                newName: "DischargeStatus");

            migrationBuilder.AddColumn<Guid>(
                name: "DischargeStatusesDischargeStatusID",
                table: "Discharges",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExaminationDetails",
                columns: table => new
                {
                    ExaminationDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DigonosisExaminationID = table.Column<int>(type: "int", nullable: false),
                    PatientExaminationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationDetails", x => x.ExaminationDetailID);
                    table.ForeignKey(
                        name: "FK_ExaminationDetails_DiagonosisExaminations_DigonosisExaminationID",
                        column: x => x.DigonosisExaminationID,
                        principalTable: "DiagonosisExaminations",
                        principalColumn: "DigonosisExaminationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationDetails_PatientExaminations_PatientExaminationID",
                        column: x => x.PatientExaminationID,
                        principalTable: "PatientExaminations",
                        principalColumn: "PatientExaminationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientDetails",
                columns: table => new
                {
                    PatientDetailsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionID = table.Column<int>(type: "int", nullable: false),
                    Allergies = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChronicIllness = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Employer = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Medication = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Relegion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDetails", x => x.PatientDetailsID);
                    table.ForeignKey(
                        name: "FK_PatientDetails_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientDetails_Regions_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regions",
                        principalColumn: "RegionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discharges_DischargeStatusesDischargeStatusID",
                table: "Discharges",
                column: "DischargeStatusesDischargeStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDetails_DigonosisExaminationID",
                table: "ExaminationDetails",
                column: "DigonosisExaminationID");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDetails_PatientExaminationID",
                table: "ExaminationDetails",
                column: "PatientExaminationID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDetails_AdmissionID",
                table: "PatientDetails",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDetails_RegionID",
                table: "PatientDetails",
                column: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Discharges_DischargeStatuses_DischargeStatusesDischargeStatusID",
                table: "Discharges",
                column: "DischargeStatusesDischargeStatusID",
                principalTable: "DischargeStatuses",
                principalColumn: "DischargeStatusID");
        }
    }
}
