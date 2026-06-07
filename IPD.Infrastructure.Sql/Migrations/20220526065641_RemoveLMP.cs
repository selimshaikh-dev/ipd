using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class RemoveLMP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LMP",
                table: "Partograph");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LMP",
                table: "Partograph",
                type: "SmallDateTime",
                nullable: true);
        }
    }
}
