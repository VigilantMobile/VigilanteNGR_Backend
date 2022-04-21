using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class trusted_persons_owner_rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustedPeople_Users_ContactId",
                table: "TrustedPeople");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                table: "TrustedPeople",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_TrustedPeople_ContactId",
                table: "TrustedPeople",
                newName: "IX_TrustedPeople_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedPeople_Users_OwnerId",
                table: "TrustedPeople",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustedPeople_Users_OwnerId",
                table: "TrustedPeople");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "TrustedPeople",
                newName: "ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_TrustedPeople_OwnerId",
                table: "TrustedPeople",
                newName: "IX_TrustedPeople_ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedPeople_Users_ContactId",
                table: "TrustedPeople",
                column: "ContactId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
