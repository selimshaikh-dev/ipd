using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class addexaminationdeta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationDetail_DiagonosisExaminations_DigonosisExaminationID",
                table: "ExaminationDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationDetail_PatientExaminations_PatientExaminationID",
                table: "ExaminationDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExaminationDetail",
                table: "ExaminationDetail");

            migrationBuilder.RenameTable(
                name: "ExaminationDetail",
                newName: "ExaminationDetails");

            migrationBuilder.RenameIndex(
                name: "IX_ExaminationDetail_PatientExaminationID",
                table: "ExaminationDetails",
                newName: "IX_ExaminationDetails_PatientExaminationID");

            migrationBuilder.RenameIndex(
                name: "IX_ExaminationDetail_DigonosisExaminationID",
                table: "ExaminationDetails",
                newName: "IX_ExaminationDetails_DigonosisExaminationID");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Discharges",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MedicationAdvice",
                table: "Discharges",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FinalDiagnosis",
                table: "Discharges",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DischargeTime",
                table: "Discharges",
                type: "SmallDateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DischargeDate",
                table: "Discharges",
                type: "SmallDateTime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "DietNutritionAdvice",
                table: "Discharges",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Advice",
                table: "Discharges",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExaminationDetails",
                table: "ExaminationDetails",
                column: "ExaminationDetailID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationDetails_DiagonosisExaminations_DigonosisExaminationID",
                table: "ExaminationDetails",
                column: "DigonosisExaminationID",
                principalTable: "DiagonosisExaminations",
                principalColumn: "DigonosisExaminationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationDetails_PatientExaminations_PatientExaminationID",
                table: "ExaminationDetails",
                column: "PatientExaminationID",
                principalTable: "PatientExaminations",
                principalColumn: "PatientExaminationID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationDetails_DiagonosisExaminations_DigonosisExaminationID",
                table: "ExaminationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationDetails_PatientExaminations_PatientExaminationID",
                table: "ExaminationDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExaminationDetails",
                table: "ExaminationDetails");

            migrationBuilder.RenameTable(
                name: "ExaminationDetails",
                newName: "ExaminationDetail");

            migrationBuilder.RenameIndex(
                name: "IX_ExaminationDetails_PatientExaminationID",
                table: "ExaminationDetail",
                newName: "IX_ExaminationDetail_PatientExaminationID");

            migrationBuilder.RenameIndex(
                name: "IX_ExaminationDetails_DigonosisExaminationID",
                table: "ExaminationDetail",
                newName: "IX_ExaminationDetail_DigonosisExaminationID");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Discharges",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "MedicationAdvice",
                table: "Discharges",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "FinalDiagnosis",
                table: "Discharges",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DischargeTime",
                table: "Discharges",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "SmallDateTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DischargeDate",
                table: "Discharges",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "SmallDateTime");

            migrationBuilder.AlterColumn<string>(
                name: "DietNutritionAdvice",
                table: "Discharges",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "Advice",
                table: "Discharges",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExaminationDetail",
                table: "ExaminationDetail",
                column: "ExaminationDetailID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationDetail_DiagonosisExaminations_DigonosisExaminationID",
                table: "ExaminationDetail",
                column: "DigonosisExaminationID",
                principalTable: "DiagonosisExaminations",
                principalColumn: "DigonosisExaminationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationDetail_PatientExaminations_PatientExaminationID",
                table: "ExaminationDetail",
                column: "PatientExaminationID",
                principalTable: "PatientExaminations",
                principalColumn: "PatientExaminationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
