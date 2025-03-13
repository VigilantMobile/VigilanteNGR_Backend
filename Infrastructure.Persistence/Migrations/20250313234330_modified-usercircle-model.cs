using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class modifiedusercirclemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustedPeople_Users_InviterteeId",
                table: "TrustedPeople");

            migrationBuilder.RenameColumn(
                name: "InviterteeId",
                table: "TrustedPeople",
                newName: "InviteeId");

            migrationBuilder.RenameIndex(
                name: "IX_TrustedPeople_InviterteeId",
                table: "TrustedPeople",
                newName: "IX_TrustedPeople_InviteeId");

            migrationBuilder.AddColumn<string>(
                name: "Relationship",
                table: "TrustedPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedPeople_Users_InviteeId",
                table: "TrustedPeople",
                column: "InviteeId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustedPeople_Users_InviteeId",
                table: "TrustedPeople");

            migrationBuilder.DropColumn(
                name: "Relationship",
                table: "TrustedPeople");

            migrationBuilder.RenameColumn(
                name: "InviteeId",
                table: "TrustedPeople",
                newName: "InviterteeId");

            migrationBuilder.RenameIndex(
                name: "IX_TrustedPeople_InviteeId",
                table: "TrustedPeople",
                newName: "IX_TrustedPeople_InviterteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedPeople_Users_InviterteeId",
                table: "TrustedPeople",
                column: "InviterteeId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
