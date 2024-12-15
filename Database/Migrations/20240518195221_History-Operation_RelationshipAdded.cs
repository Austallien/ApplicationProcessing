using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class HistoryOperation_RelationshipAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OperationId",
                table: "History",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_History_OperationId",
                table: "History",
                column: "OperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Operation_OperationId",
                table: "History",
                column: "OperationId",
                principalTable: "Operation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Operation_OperationId",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_OperationId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "OperationId",
                table: "History");
        }
    }
}
