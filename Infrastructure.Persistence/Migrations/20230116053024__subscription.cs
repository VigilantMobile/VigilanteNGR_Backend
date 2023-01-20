using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class _subscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_BroadcastLevels_LocationLevelId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LocationLevelId",
                table: "Users",
                newName: "BroadcastLevelId");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Users",
                newName: "TownId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_LocationLevelId",
                table: "Users",
                newName: "IX_Users_BroadcastLevelId");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyFee = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SubscriptionId",
                table: "Users",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TownId",
                table: "Users",
                column: "TownId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BroadcastLevels_BroadcastLevelId",
                table: "Users",
                column: "BroadcastLevelId",
                principalTable: "BroadcastLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Subscriptions_SubscriptionId",
                table: "Users",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Towns_TownId",
                table: "Users",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_BroadcastLevels_BroadcastLevelId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Subscriptions_SubscriptionId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Towns_TownId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Users_SubscriptionId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TownId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TownId",
                table: "Users",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "BroadcastLevelId",
                table: "Users",
                newName: "LocationLevelId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_BroadcastLevelId",
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
    }
}
