using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class AddedLMPInPartograph : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Para",
                table: "Partograph");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EDD",
                table: "Partograph",
                type: "SmallDateTime",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LMP",
                table: "Partograph",
                type: "SmallDateTime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LMP",
                table: "Partograph");

            migrationBuilder.AlterColumn<int>(
                name: "EDD",
                table: "Partograph",
                type: "int",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "SmallDateTime",
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Para",
                table: "Partograph",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
