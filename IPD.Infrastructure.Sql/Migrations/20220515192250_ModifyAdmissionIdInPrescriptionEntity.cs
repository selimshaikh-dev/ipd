using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class ModifyAdmissionIdInPrescriptionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPlans_Admissions_AdmissionID",
                table: "MedicationPlans");

            migrationBuilder.DropIndex(
                name: "IX_MedicationPlans_AdmissionID",
                table: "MedicationPlans");

            migrationBuilder.DropColumn(
                name: "AdmissionID",
                table: "MedicationPlans");

            migrationBuilder.AddColumn<Guid>(
                name: "AdmissionID",
                table: "Prescriptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_AdmissionID",
                table: "Prescriptions",
                column: "AdmissionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Admissions_AdmissionID",
                table: "Prescriptions",
                column: "AdmissionID",
                principalTable: "Admissions",
                principalColumn: "AdmissionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Admissions_AdmissionID",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_AdmissionID",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "AdmissionID",
                table: "Prescriptions");

            migrationBuilder.AddColumn<Guid>(
                name: "AdmissionID",
                table: "MedicationPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPlans_AdmissionID",
                table: "MedicationPlans",
                column: "AdmissionID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPlans_Admissions_AdmissionID",
                table: "MedicationPlans",
                column: "AdmissionID",
                principalTable: "Admissions",
                principalColumn: "AdmissionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
