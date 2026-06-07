using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class CreateDiagonosisDetailstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chiefdoms_Admissions_AdmissionID",
                table: "Chiefdoms");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientDiagnosis_ICDDigonosisCodes_DiseaseID",
                table: "PatientDiagnosis");

            migrationBuilder.DropIndex(
                name: "IX_PatientDiagnosis_DiseaseID",
                table: "PatientDiagnosis");

            migrationBuilder.DropIndex(
                name: "IX_Chiefdoms_AdmissionID",
                table: "Chiefdoms");

            migrationBuilder.DropColumn(
                name: "DiseaseID",
                table: "PatientDiagnosis");

            migrationBuilder.DropColumn(
                name: "AdmissionID",
                table: "Chiefdoms");

            migrationBuilder.CreateTable(
                name: "DiagonosisDetails",
                columns: table => new
                {
                    DiagonosisDetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseID = table.Column<int>(type: "int", nullable: false),
                    PatientDiagnosisID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagonosisDetails", x => x.DiagonosisDetailsID);
                    table.ForeignKey(
                        name: "FK_DiagonosisDetails_ICDDigonosisCodes_DiseaseID",
                        column: x => x.DiseaseID,
                        principalTable: "ICDDigonosisCodes",
                        principalColumn: "DiseaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagonosisDetails_PatientDiagnosis_PatientDiagnosisID",
                        column: x => x.PatientDiagnosisID,
                        principalTable: "PatientDiagnosis",
                        principalColumn: "PatientDiagnosisID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiagonosisDetails_DiseaseID",
                table: "DiagonosisDetails",
                column: "DiseaseID");

            migrationBuilder.CreateIndex(
                name: "IX_DiagonosisDetails_PatientDiagnosisID",
                table: "DiagonosisDetails",
                column: "PatientDiagnosisID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiagonosisDetails");

            migrationBuilder.AddColumn<int>(
                name: "DiseaseID",
                table: "PatientDiagnosis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AdmissionID",
                table: "Chiefdoms",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiagnosis_DiseaseID",
                table: "PatientDiagnosis",
                column: "DiseaseID");

            migrationBuilder.CreateIndex(
                name: "IX_Chiefdoms_AdmissionID",
                table: "Chiefdoms",
                column: "AdmissionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Chiefdoms_Admissions_AdmissionID",
                table: "Chiefdoms",
                column: "AdmissionID",
                principalTable: "Admissions",
                principalColumn: "AdmissionID");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDiagnosis_ICDDigonosisCodes_DiseaseID",
                table: "PatientDiagnosis",
                column: "DiseaseID",
                principalTable: "ICDDigonosisCodes",
                principalColumn: "DiseaseID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
