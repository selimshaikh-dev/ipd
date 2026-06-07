using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class modifypatientsDiagonosisEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientDiagnosis_ICDDigonosisCodes_ICDDigonosisCodesDiseaseID",
                table: "PatientDiagnosis");

            migrationBuilder.DropIndex(
                name: "IX_PatientDiagnosis_ICDDigonosisCodesDiseaseID",
                table: "PatientDiagnosis");

            migrationBuilder.DropColumn(
                name: "ICDDigonosisCodesDiseaseID",
                table: "PatientDiagnosis");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiagnosis_DiseaseID",
                table: "PatientDiagnosis",
                column: "DiseaseID");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDiagnosis_ICDDigonosisCodes_DiseaseID",
                table: "PatientDiagnosis",
                column: "DiseaseID",
                principalTable: "ICDDigonosisCodes",
                principalColumn: "DiseaseID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientDiagnosis_ICDDigonosisCodes_DiseaseID",
                table: "PatientDiagnosis");

            migrationBuilder.DropIndex(
                name: "IX_PatientDiagnosis_DiseaseID",
                table: "PatientDiagnosis");

            migrationBuilder.AddColumn<int>(
                name: "ICDDigonosisCodesDiseaseID",
                table: "PatientDiagnosis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiagnosis_ICDDigonosisCodesDiseaseID",
                table: "PatientDiagnosis",
                column: "ICDDigonosisCodesDiseaseID");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDiagnosis_ICDDigonosisCodes_ICDDigonosisCodesDiseaseID",
                table: "PatientDiagnosis",
                column: "ICDDigonosisCodesDiseaseID",
                principalTable: "ICDDigonosisCodes",
                principalColumn: "DiseaseID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
