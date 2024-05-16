using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class changedWalletToUserSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Users_ApplicationUserId",
                table: "Wallets");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Users_ApplicationUserId",
                table: "Wallets",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Users_ApplicationUserId",
                table: "Wallets");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Users_ApplicationUserId",
                table: "Wallets",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
