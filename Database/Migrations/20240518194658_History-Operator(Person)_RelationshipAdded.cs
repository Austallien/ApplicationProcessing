using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class HistoryOperatorPerson_RelationshipAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OperatorId",
                table: "History",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_History_OperatorId",
                table: "History",
                column: "OperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Person_OperatorId",
                table: "History",
                column: "OperatorId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Person_OperatorId",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_OperatorId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "OperatorId",
                table: "History");
        }
    }
}
