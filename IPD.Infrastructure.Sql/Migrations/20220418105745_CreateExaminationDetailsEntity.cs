using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class CreateExaminationDetailsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientExaminations_DiagonosisExaminations_DigonosisExaminationID",
                table: "PatientExaminations");

            migrationBuilder.DropIndex(
                name: "IX_PatientExaminations_DigonosisExaminationID",
                table: "PatientExaminations");

            migrationBuilder.DropColumn(
                name: "DigonosisExaminationID",
                table: "PatientExaminations");

            migrationBuilder.CreateTable(
                name: "ExaminationDetails",
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

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDetails_DigonosisExaminationID",
                table: "ExaminationDetails",
                column: "DigonosisExaminationID");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationDetails_PatientExaminationID",
                table: "ExaminationDetails",
                column: "PatientExaminationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationDetails");

            migrationBuilder.AddColumn<int>(
                name: "DigonosisExaminationID",
                table: "PatientExaminations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminations_DigonosisExaminationID",
                table: "PatientExaminations",
                column: "DigonosisExaminationID");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientExaminations_DiagonosisExaminations_DigonosisExaminationID",
                table: "PatientExaminations",
                column: "DigonosisExaminationID",
                principalTable: "DiagonosisExaminations",
                principalColumn: "DigonosisExaminationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
