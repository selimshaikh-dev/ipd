using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class Removesmalldatetimecolums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "volumes",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Temperatures",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Pulses",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Proteins",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OxytocinTime",
                table: "Oxytocins",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MouldingTime",
                table: "Mouldings",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Medicines",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LiquorTime",
                table: "Liquors",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FetalRateTime",
                table: "FetalHeartRates",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DropsTime",
                table: "Drops",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DescentOfHeadTime",
                table: "DescentOfHeads",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractionsTime",
                table: "Contractions",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CervixTime",
                table: "Cervixes",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "BloodPressures",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Acetones",
                type: "SmallDateTime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
