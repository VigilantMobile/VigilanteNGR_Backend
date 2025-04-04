using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refactoredSecurityTipentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EscalatedTips_SecurityTips_SecurityTipId",
                table: "EscalatedTips");

            migrationBuilder.DropForeignKey(
                name: "FK_EscalatedTips_Users_EscalationAuthorizerID",
                table: "EscalatedTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_BroadcastLevels_BroadcastLevelId",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_SecurityTipStatuses_SecurityTipStatusId",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_Towns_TownId",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_Users_AdminAuthorizerID",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_Users_ExternalAuthorizerId",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_Users_ExternalInitiatorId",
                table: "SecurityTips");

            migrationBuilder.DropTable(
                name: "SecurityTipStatuses");

            migrationBuilder.DropIndex(
                name: "IX_SecurityTips_AdminAuthorizerID",
                table: "SecurityTips");

            migrationBuilder.DropIndex(
                name: "IX_SecurityTips_ExternalAuthorizerId",
                table: "SecurityTips");

            migrationBuilder.DropIndex(
                name: "IX_SecurityTips_ExternalInitiatorId",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "AdminAuthorizerID",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "BroadcasterTypeString",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "EscalationRequested",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "ExternalAuthorizerId",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "ExternalInitiatorId",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "TipStatusString",
                table: "SecurityTips");

            migrationBuilder.RenameColumn(
                name: "SecurityTipStatusId",
                table: "SecurityTips",
                newName: "TownId1");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "SecurityTips",
                newName: "Coordinates");

            migrationBuilder.RenameColumn(
                name: "IsAdminAuthorized",
                table: "SecurityTips",
                newName: "IsOngoing");

            migrationBuilder.RenameIndex(
                name: "IX_SecurityTips_SecurityTipStatusId",
                table: "SecurityTips",
                newName: "IX_SecurityTips_TownId1");

            migrationBuilder.AlterColumn<Guid>(
                name: "TownId",
                table: "SecurityTips",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BroadcastLevelId",
                table: "SecurityTips",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "BroadcasterType",
                table: "SecurityTips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "IncidentDateTime",
                table: "SecurityTips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ShareCount",
                table: "SecurityTips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SecurityTips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "SecurityTips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_EscalatedTips_SecurityTips_SecurityTipId",
                table: "EscalatedTips",
                column: "SecurityTipId",
                principalTable: "SecurityTips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EscalatedTips_Users_EscalationAuthorizerID",
                table: "EscalatedTips",
                column: "EscalationAuthorizerID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_BroadcastLevels_BroadcastLevelId",
                table: "SecurityTips",
                column: "BroadcastLevelId",
                principalTable: "BroadcastLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_Sources_TownId",
                table: "SecurityTips",
                column: "TownId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_Towns_TownId1",
                table: "SecurityTips",
                column: "TownId1",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EscalatedTips_SecurityTips_SecurityTipId",
                table: "EscalatedTips");

            migrationBuilder.DropForeignKey(
                name: "FK_EscalatedTips_Users_EscalationAuthorizerID",
                table: "EscalatedTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_BroadcastLevels_BroadcastLevelId",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_Sources_TownId",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_Towns_TownId1",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "BroadcasterType",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "IncidentDateTime",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "ShareCount",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "SecurityTips");

            migrationBuilder.RenameColumn(
                name: "TownId1",
                table: "SecurityTips",
                newName: "SecurityTipStatusId");

            migrationBuilder.RenameColumn(
                name: "IsOngoing",
                table: "SecurityTips",
                newName: "IsAdminAuthorized");

            migrationBuilder.RenameColumn(
                name: "Coordinates",
                table: "SecurityTips",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_SecurityTips_TownId1",
                table: "SecurityTips",
                newName: "IX_SecurityTips_SecurityTipStatusId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TownId",
                table: "SecurityTips",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BroadcastLevelId",
                table: "SecurityTips",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminAuthorizerID",
                table: "SecurityTips",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BroadcasterTypeString",
                table: "SecurityTips",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EscalationRequested",
                table: "SecurityTips",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ExternalAuthorizerId",
                table: "SecurityTips",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalInitiatorId",
                table: "SecurityTips",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipStatusString",
                table: "SecurityTips",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SecurityTipStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TipStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityTipStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_AdminAuthorizerID",
                table: "SecurityTips",
                column: "AdminAuthorizerID");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_ExternalAuthorizerId",
                table: "SecurityTips",
                column: "ExternalAuthorizerId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_ExternalInitiatorId",
                table: "SecurityTips",
                column: "ExternalInitiatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_EscalatedTips_SecurityTips_SecurityTipId",
                table: "EscalatedTips",
                column: "SecurityTipId",
                principalTable: "SecurityTips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EscalatedTips_Users_EscalationAuthorizerID",
                table: "EscalatedTips",
                column: "EscalationAuthorizerID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_BroadcastLevels_BroadcastLevelId",
                table: "SecurityTips",
                column: "BroadcastLevelId",
                principalTable: "BroadcastLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_SecurityTipStatuses_SecurityTipStatusId",
                table: "SecurityTips",
                column: "SecurityTipStatusId",
                principalTable: "SecurityTipStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_Towns_TownId",
                table: "SecurityTips",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_Users_AdminAuthorizerID",
                table: "SecurityTips",
                column: "AdminAuthorizerID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_Users_ExternalAuthorizerId",
                table: "SecurityTips",
                column: "ExternalAuthorizerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_Users_ExternalInitiatorId",
                table: "SecurityTips",
                column: "ExternalInitiatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
