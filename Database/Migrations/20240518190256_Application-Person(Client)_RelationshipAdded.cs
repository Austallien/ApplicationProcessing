using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class ApplicationPersonClient_RelationshipAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                table: "Application",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Application_ClientId",
                table: "Application",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Person_ClientId",
                table: "Application",
                column: "ClientId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Person_ClientId",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_ClientId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Application");
        }
    }
}
