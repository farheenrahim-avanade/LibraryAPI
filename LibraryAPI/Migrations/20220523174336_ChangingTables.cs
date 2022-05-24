using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    public partial class ChangingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryItems_Users_UserId",
                table: "LibraryItems");

            migrationBuilder.DropIndex(
                name: "IX_LibraryItems_UserId",
                table: "LibraryItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LibraryItems_UserId",
                table: "LibraryItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryItems_Users_UserId",
                table: "LibraryItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
