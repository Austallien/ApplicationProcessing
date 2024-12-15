using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class ContactMethodsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ContactMethod",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1L, "Phone" });

            migrationBuilder.InsertData(
                table: "ContactMethod",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2L, "Email" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ContactMethod",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ContactMethod",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
