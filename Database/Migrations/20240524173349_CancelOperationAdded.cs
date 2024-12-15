using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class CancelOperationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Operation",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4L, "Cancel" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Operation",
                keyColumn: "Id",
                keyValue: 4L);
        }
    }
}
