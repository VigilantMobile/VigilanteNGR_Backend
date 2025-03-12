using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TrustedContact_renamed_inviter_and_invitee_names_and_ids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustedPeople_Users_TrustedUserId",
                table: "TrustedPeople");

            migrationBuilder.RenameColumn(
                name: "TrustedUserId",
                table: "TrustedPeople",
                newName: "InviterteeId");

            migrationBuilder.RenameIndex(
                name: "IX_TrustedPeople_TrustedUserId",
                table: "TrustedPeople",
                newName: "IX_TrustedPeople_InviterteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedPeople_Users_InviterteeId",
                table: "TrustedPeople",
                column: "InviterteeId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustedPeople_Users_InviterteeId",
                table: "TrustedPeople");

            migrationBuilder.RenameColumn(
                name: "InviterteeId",
                table: "TrustedPeople",
                newName: "TrustedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TrustedPeople_InviterteeId",
                table: "TrustedPeople",
                newName: "IX_TrustedPeople_TrustedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedPeople_Users_TrustedUserId",
                table: "TrustedPeople",
                column: "TrustedUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
