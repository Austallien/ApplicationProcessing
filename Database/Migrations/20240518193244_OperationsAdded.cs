using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class OperationsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Operation",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1L, "Creation" });

            migrationBuilder.InsertData(
                table: "Operation",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2L, "Deletion" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Operation",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Operation",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
