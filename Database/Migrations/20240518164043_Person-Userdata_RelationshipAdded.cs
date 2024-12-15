using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class PersonUserdata_RelationshipAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PersonId",
                table: "Userdata",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Userdata_PersonId",
                table: "Userdata",
                column: "PersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Userdata_Person_PersonId",
                table: "Userdata",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Userdata_Person_PersonId",
                table: "Userdata");

            migrationBuilder.DropIndex(
                name: "IX_Userdata_PersonId",
                table: "Userdata");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Userdata");
        }
    }
}