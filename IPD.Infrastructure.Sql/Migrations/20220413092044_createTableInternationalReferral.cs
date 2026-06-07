using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class createTableInternationalReferral : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ComplaintName",
                table: "Complaints",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "InternationalReferrals",
                columns: table => new
                {
                    InternationalReferralID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phalala = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CivilServent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmploymentNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReferralType = table.Column<byte>(type: "tinyint", nullable: false),
                    ReferringSpecialist = table.Column<string>(type: "nvarchar(92)", maxLength: 92, nullable: false),
                    PracticeNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Discipline = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReasonReferral = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ShortHistory = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Investigation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ContactDetails = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Date = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Time = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    PatientsTransferApparatus = table.Column<byte>(type: "tinyint", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IDNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Relegion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Employer = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Allergies = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChronicIllness = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Medication = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProcedureID = table.Column<int>(type: "int", nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionID = table.Column<int>(type: "int", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternationalReferrals", x => x.InternationalReferralID);
                    table.ForeignKey(
                        name: "FK_InternationalReferrals_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternationalReferrals_Procedure_ProcedureID",
                        column: x => x.ProcedureID,
                        principalTable: "Procedure",
                        principalColumn: "ProcedureID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternationalReferrals_Regions_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regions",
                        principalColumn: "RegionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partograph",
                columns: table => new
                {
                    PartographID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gravida = table.Column<byte>(type: "tinyint", nullable: false),
                    Parity = table.Column<byte>(type: "tinyint", nullable: false),
                    SBOrNND = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Abortion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EDD = table.Column<int>(type: "int", nullable: false),
                    BorderlineRiskFactors = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RegularContractions = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    MembranesRuptured = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partograph", x => x.PartographID);
                    table.ForeignKey(
                        name: "FK_Partograph_Admissions_AdmissionID",
                        column: x => x.AdmissionID,
                        principalTable: "Admissions",
                        principalColumn: "AdmissionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartographDetails",
                columns: table => new
                {
                    PartographDetailsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartographID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Liquor = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LiquorTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Moulding = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MouldingTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Cervix = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CervixTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    DescentOfHead = table.Column<int>(type: "int", nullable: false),
                    DescentOfHeadTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Contractions = table.Column<int>(type: "int", nullable: false),
                    ContractionsDuration = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContractionsTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    OxytocinUL = table.Column<int>(type: "int", nullable: false),
                    OxytocinTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Drops = table.Column<int>(type: "int", nullable: false),
                    DropsTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Medicine = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MedicineTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Systolic = table.Column<int>(type: "int", nullable: false),
                    Diastolic = table.Column<int>(type: "int", nullable: false),
                    BpTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Pulse = table.Column<int>(type: "int", nullable: false),
                    PulseTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Temp = table.Column<int>(type: "int", nullable: false),
                    TempTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Protein = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ProteinTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Acetone = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AcetoneTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false),
                    VolumeTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    FetalRate = table.Column<int>(type: "int", nullable: false),
                    FetalRateTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartographDetails", x => x.PartographDetailsID);
                    table.ForeignKey(
                        name: "FK_PartographDetails_Partograph_PartographID",
                        column: x => x.PartographID,
                        principalTable: "Partograph",
                        principalColumn: "PartographID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternationalReferrals_AdmissionID",
                table: "InternationalReferrals",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_InternationalReferrals_ProcedureID",
                table: "InternationalReferrals",
                column: "ProcedureID");

            migrationBuilder.CreateIndex(
                name: "IX_InternationalReferrals_RegionID",
                table: "InternationalReferrals",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Partograph_AdmissionID",
                table: "Partograph",
                column: "AdmissionID");

            migrationBuilder.CreateIndex(
                name: "IX_PartographDetails_PartographID",
                table: "PartographDetails",
                column: "PartographID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternationalReferrals");

            migrationBuilder.DropTable(
                name: "PartographDetails");

            migrationBuilder.DropTable(
                name: "Partograph");

            migrationBuilder.AlterColumn<string>(
                name: "ComplaintName",
                table: "Complaints",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);
        }
    }
}
