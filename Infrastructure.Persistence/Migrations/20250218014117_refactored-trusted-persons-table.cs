using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refactoredtrustedpersonstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustedPeople_Users_OwnerId",
                table: "TrustedPeople");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "TrustedPeople");

            migrationBuilder.DropColumn(
                name: "TownId",
                table: "TrustedPeople");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "TrustedPeople",
                newName: "TrustedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TrustedPeople_OwnerId",
                table: "TrustedPeople",
                newName: "IX_TrustedPeople_TrustedUserId");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "TrustedPeople",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InviterId",
                table: "TrustedPeople",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TrustedPeople",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TrustedPeople_InviterId",
                table: "TrustedPeople",
                column: "InviterId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedPeople_Users_InviterId",
                table: "TrustedPeople",
                column: "InviterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedPeople_Users_TrustedUserId",
                table: "TrustedPeople",
                column: "TrustedUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrustedPeople_Users_InviterId",
                table: "TrustedPeople");

            migrationBuilder.DropForeignKey(
                name: "FK_TrustedPeople_Users_TrustedUserId",
                table: "TrustedPeople");

            migrationBuilder.DropIndex(
                name: "IX_TrustedPeople_InviterId",
                table: "TrustedPeople");

            migrationBuilder.DropColumn(
                name: "InviterId",
                table: "TrustedPeople");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TrustedPeople");

            migrationBuilder.RenameColumn(
                name: "TrustedUserId",
                table: "TrustedPeople",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_TrustedPeople_TrustedUserId",
                table: "TrustedPeople",
                newName: "IX_TrustedPeople_OwnerId");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "TrustedPeople",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "TrustedPeople",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TownId",
                table: "TrustedPeople",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_TrustedPeople_Users_OwnerId",
                table: "TrustedPeople",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
