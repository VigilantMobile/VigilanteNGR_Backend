using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class genericlocationIdfor_applicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_LGAs_LGAId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Settlements_SettlementId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_States_StateId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Towns_TownId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LGAId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SettlementId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_StateId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LGAId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SettlementId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TownId",
                table: "Users",
                newName: "LocationLevelId");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "Users",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_TownId",
                table: "Users",
                newName: "IX_Users_LocationLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BroadcastLevels_LocationLevelId",
                table: "Users",
                column: "LocationLevelId",
                principalTable: "BroadcastLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_BroadcastLevels_LocationLevelId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LocationLevelId",
                table: "Users",
                newName: "TownId");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Users",
                newName: "StateId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_LocationLevelId",
                table: "Users",
                newName: "IX_Users_TownId");

            migrationBuilder.AddColumn<int>(
                name: "LGAId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SettlementId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LGAId",
                table: "Users",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SettlementId",
                table: "Users",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StateId",
                table: "Users",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LGAs_LGAId",
                table: "Users",
                column: "LGAId",
                principalTable: "LGAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Settlements_SettlementId",
                table: "Users",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_States_StateId",
                table: "Users",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Towns_TownId",
                table: "Users",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
