using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class ModifyPartographDetailsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OxytocinUL",
                table: "PartographDetails",
                newName: "Oxytocin");

            migrationBuilder.AlterColumn<string>(
                name: "CervixDetails",
                table: "Cervixes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Oxytocin",
                table: "PartographDetails",
                newName: "OxytocinUL");

            migrationBuilder.AlterColumn<int>(
                name: "CervixDetails",
                table: "Cervixes",
                type: "int",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
