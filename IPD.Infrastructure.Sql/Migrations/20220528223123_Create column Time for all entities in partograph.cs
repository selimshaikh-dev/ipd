using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class CreatecolumnTimeforallentitiesinpartograph : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Time",
                table: "volumes",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Time",
                table: "Temperatures",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Time",
                table: "Pulses",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Time",
                table: "Proteins",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "InitiateDate",
                table: "Partograph",
                type: "BigInt",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OxytocinTime",
                table: "Oxytocins",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MouldingTime",
                table: "Mouldings",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Time",
                table: "Medicines",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "LiquorTime",
                table: "Liquors",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FetalRateTime",
                table: "FetalHeartRates",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DropsTime",
                table: "Drops",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DescentOfHeadTime",
                table: "DescentOfHeads",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ContractionsTime",
                table: "Contractions",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CervixTime",
                table: "Cervixes",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Time",
                table: "BloodPressures",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Time",
                table: "Acetones",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "volumes");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Temperatures");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Pulses");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Proteins");

            migrationBuilder.DropColumn(
                name: "InitiateDate",
                table: "Partograph");

            migrationBuilder.DropColumn(
                name: "OxytocinTime",
                table: "Oxytocins");

            migrationBuilder.DropColumn(
                name: "MouldingTime",
                table: "Mouldings");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "LiquorTime",
                table: "Liquors");

            migrationBuilder.DropColumn(
                name: "FetalRateTime",
                table: "FetalHeartRates");

            migrationBuilder.DropColumn(
                name: "DropsTime",
                table: "Drops");

            migrationBuilder.DropColumn(
                name: "DescentOfHeadTime",
                table: "DescentOfHeads");

            migrationBuilder.DropColumn(
                name: "ContractionsTime",
                table: "Contractions");

            migrationBuilder.DropColumn(
                name: "CervixTime",
                table: "Cervixes");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "BloodPressures");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Acetones");
        }
    }
}
