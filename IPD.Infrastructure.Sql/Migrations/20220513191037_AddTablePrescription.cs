using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class AddTablePrescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PrescriptionsID",
                table: "MedicationPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrescriptionsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionsID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPlans_PrescriptionsID",
                table: "MedicationPlans",
                column: "PrescriptionsID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationPlans_Prescriptions_PrescriptionsID",
                table: "MedicationPlans",
                column: "PrescriptionsID",
                principalTable: "Prescriptions",
                principalColumn: "PrescriptionsID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationPlans_Prescriptions_PrescriptionsID",
                table: "MedicationPlans");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_MedicationPlans_PrescriptionsID",
                table: "MedicationPlans");

            migrationBuilder.DropColumn(
                name: "PrescriptionsID",
                table: "MedicationPlans");
        }
    }
}
