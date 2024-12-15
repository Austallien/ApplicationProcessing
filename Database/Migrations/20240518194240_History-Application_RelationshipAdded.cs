using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class HistoryApplication_RelationshipAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ApplicationId",
                table: "History",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_History_ApplicationId",
                table: "History",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Application_ApplicationId",
                table: "History",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Application_ApplicationId",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_ApplicationId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "History");
        }
    }
}
