using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class removepartographdetailsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartographDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartographDetails",
                columns: table => new
                {
                    PartographDetailsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartographID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Acetone = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AcetoneTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    BpTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Cervix = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CervixTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Contractions = table.Column<int>(type: "int", nullable: false),
                    ContractionsDuration = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContractionsTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DateModified = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DescentOfHead = table.Column<int>(type: "int", nullable: false),
                    DescentOfHeadTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Diastolic = table.Column<int>(type: "int", nullable: false),
                    Drops = table.Column<int>(type: "int", nullable: false),
                    DropsTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    FacilityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FetalRate = table.Column<int>(type: "int", nullable: false),
                    FetalRateTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    IsRowDeleted = table.Column<bool>(type: "bit", nullable: true),
                    Liquor = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LiquorTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Medicine = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MedicineTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Moulding = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MouldingTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Oxytocin = table.Column<int>(type: "int", nullable: false),
                    OxytocinTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Protein = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ProteinTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Pulse = table.Column<int>(type: "int", nullable: false),
                    PulseTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Systolic = table.Column<int>(type: "int", nullable: false),
                    Temp = table.Column<int>(type: "int", nullable: false),
                    TempTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false),
                    VolumeTime = table.Column<DateTime>(type: "SmallDateTime", nullable: false)
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
                name: "IX_PartographDetails_PartographID",
                table: "PartographDetails",
                column: "PartographID");
        }
    }
}
