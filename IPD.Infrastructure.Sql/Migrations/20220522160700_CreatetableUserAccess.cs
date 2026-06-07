using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPD.Infrastructure.Sql.Migrations
{
    public partial class CreatetableUserAccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccess",
                columns: table => new
                {
                    UserAccessID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Module = table.Column<byte>(type: "tinyint", nullable: false),
                    UserAccountID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SyncStatus = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccess", x => x.UserAccessID);
                    table.ForeignKey(
                        name: "FK_UserAccess_UserAccounts_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccounts",
                        principalColumn: "UserAccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccess_UserAccountID",
                table: "UserAccess",
                column: "UserAccountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccess");
        }
    }
}
