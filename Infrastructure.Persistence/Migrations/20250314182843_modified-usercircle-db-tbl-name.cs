using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class modifiedusercircledbtblname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustedPeople_Users_InviteeId",
                table: "TrustedPeople");

            migrationBuilder.DropForeignKey(
                name: "FK_TrustedPeople_Users_InviterId",
                table: "TrustedPeople");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrustedPeople",
                table: "TrustedPeople");

            migrationBuilder.RenameTable(
                name: "TrustedPeople",
                newName: "UserCircle");

            migrationBuilder.RenameIndex(
                name: "IX_TrustedPeople_InviterId",
                table: "UserCircle",
                newName: "IX_UserCircle_InviterId");

            migrationBuilder.RenameIndex(
                name: "IX_TrustedPeople_InviteeId",
                table: "UserCircle",
                newName: "IX_UserCircle_InviteeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCircle",
                table: "UserCircle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCircle_Users_InviteeId",
                table: "UserCircle",
                column: "InviteeId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCircle_Users_InviterId",
                table: "UserCircle",
                column: "InviterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCircle_Users_InviteeId",
                table: "UserCircle");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCircle_Users_InviterId",
                table: "UserCircle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCircle",
                table: "UserCircle");

            migrationBuilder.RenameTable(
                name: "UserCircle",
                newName: "TrustedPeople");

            migrationBuilder.RenameIndex(
                name: "IX_UserCircle_InviterId",
                table: "TrustedPeople",
                newName: "IX_TrustedPeople_InviterId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCircle_InviteeId",
                table: "TrustedPeople",
                newName: "IX_TrustedPeople_InviteeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrustedPeople",
                table: "TrustedPeople",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedPeople_Users_InviteeId",
                table: "TrustedPeople",
                column: "InviteeId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedPeople_Users_InviterId",
                table: "TrustedPeople",
                column: "InviterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
