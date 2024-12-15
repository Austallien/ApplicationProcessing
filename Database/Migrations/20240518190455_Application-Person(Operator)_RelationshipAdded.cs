using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class ApplicationPersonOperator_RelationshipAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OperatorId",
                table: "Application",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Application_OperatorId",
                table: "Application",
                column: "OperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_Person_OperatorId",
                table: "Application",
                column: "OperatorId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_Person_OperatorId",
                table: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Application_OperatorId",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "OperatorId",
                table: "Application");
        }
    }
}
