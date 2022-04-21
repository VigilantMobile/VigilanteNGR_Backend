using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class entities_refactor_persistence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_VGNGAStaff_HodId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_VGNGAStaff_SecretaryId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_MissingItem_VGNGAStaff_AdminAuthorizerID",
                table: "MissingItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MissingPerson_VGNGAStaff_AdminAuthorizerID",
                table: "MissingPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_Users_BroadcasterId",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_VGNGAStaff_VGNGAAdminAuthorizerId",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_StateCurfew_States_StateId",
                table: "StateCurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_StateCurfew_VGNGAStaff_AdminAuthorizerID",
                table: "StateCurfew");

            migrationBuilder.DropTable(
                name: "LGACurfew");

            migrationBuilder.DropTable(
                name: "LGAVGNGAStaff");

            migrationBuilder.DropTable(
                name: "LGAVGNGAStaff1");

            migrationBuilder.DropTable(
                name: "NPFLGAStaff");

            migrationBuilder.DropTable(
                name: "NPFSettlementAdmin");

            migrationBuilder.DropTable(
                name: "NPFStateStaff");

            migrationBuilder.DropTable(
                name: "NPFTownStaff");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteLGAStaff");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteSettlementStaff");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteStateStaff");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteTownStaff");

            migrationBuilder.DropTable(
                name: "SettlementCurfew");

            migrationBuilder.DropTable(
                name: "SettlementVGNGAStaff");

            migrationBuilder.DropTable(
                name: "SettlementVGNGAStaff1");

            migrationBuilder.DropTable(
                name: "StateVGNGAStaff");

            migrationBuilder.DropTable(
                name: "StateVGNGAStaff1");

            migrationBuilder.DropTable(
                name: "TownCurfew");

            migrationBuilder.DropTable(
                name: "TownVGNGAStaff");

            migrationBuilder.DropTable(
                name: "TownVGNGAStaff1");

            migrationBuilder.DropTable(
                name: "VGNGAStaff");

            migrationBuilder.DropIndex(
                name: "IX_MissingItem_AdminAuthorizerID",
                table: "MissingItem");

            migrationBuilder.RenameColumn(
                name: "LastLocationCoordinates",
                table: "Users",
                newName: "StaffId");

            migrationBuilder.RenameColumn(
                name: "ActiveStatus",
                table: "Users",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "AdminAuthorizerID",
                table: "StateCurfew",
                newName: "AdminAuthorizerId");

            migrationBuilder.RenameIndex(
                name: "IX_StateCurfew_AdminAuthorizerID",
                table: "StateCurfew",
                newName: "IX_StateCurfew_AdminAuthorizerId");

            migrationBuilder.RenameColumn(
                name: "VGNGAAdminAuthorizerId",
                table: "SecurityTips",
                newName: "ExternalInitiatorId");

            migrationBuilder.RenameIndex(
                name: "IX_SecurityTips_VGNGAAdminAuthorizerId",
                table: "SecurityTips",
                newName: "IX_SecurityTips_ExternalInitiatorId");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExternalStaffType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAppAdmin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAppOperator",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAppSuperAdmin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExternalAdmin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExternalOperator",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsExternalSuperAdmin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Users",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SalaryCurrency",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "StateCurfew",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AdminAuthorizerId",
                table: "StateCurfew",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<bool>(
                name: "IsAuthorized",
                table: "StateCurfew",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LGAId",
                table: "StateCurfew",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OperatorIniatorId",
                table: "StateCurfew",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SettlementId",
                table: "StateCurfew",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TownId",
                table: "StateCurfew",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminAuthorizerID",
                table: "SecurityTips",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalAuthorizerId",
                table: "SecurityTips",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TownId",
                table: "MissingItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SettlementId",
                table: "MissingItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AdminAuthorizerID",
                table: "MissingItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VGNGAStaffId",
                table: "MissingItem",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUserLGA",
                columns: table => new
                {
                    InternalStaffLGAsId = table.Column<int>(type: "int", nullable: false),
                    VGNGALGAStaffId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserLGA", x => new { x.InternalStaffLGAsId, x.VGNGALGAStaffId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserLGA_LGAs_InternalStaffLGAsId",
                        column: x => x.InternalStaffLGAsId,
                        principalTable: "LGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserLGA_Users_VGNGALGAStaffId",
                        column: x => x.VGNGALGAStaffId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserSettlement",
                columns: table => new
                {
                    InternalStaffSettlementsId = table.Column<int>(type: "int", nullable: false),
                    VGNGASettlementStaffId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserSettlement", x => new { x.InternalStaffSettlementsId, x.VGNGASettlementStaffId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserSettlement_Settlements_InternalStaffSettlementsId",
                        column: x => x.InternalStaffSettlementsId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserSettlement_Users_VGNGASettlementStaffId",
                        column: x => x.VGNGASettlementStaffId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserState",
                columns: table => new
                {
                    InternalStaffStatesId = table.Column<int>(type: "int", nullable: false),
                    VGNGAStateStaffId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserState", x => new { x.InternalStaffStatesId, x.VGNGAStateStaffId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserState_States_InternalStaffStatesId",
                        column: x => x.InternalStaffStatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserState_Users_VGNGAStateStaffId",
                        column: x => x.VGNGAStateStaffId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserTown",
                columns: table => new
                {
                    InternalStaffTownsId = table.Column<int>(type: "int", nullable: false),
                    VGNGATownStaffId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserTown", x => new { x.InternalStaffTownsId, x.VGNGATownStaffId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserTown_Towns_InternalStaffTownsId",
                        column: x => x.InternalStaffTownsId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserTown_Users_VGNGATownStaffId",
                        column: x => x.VGNGATownStaffId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StateCurfew_LGAId",
                table: "StateCurfew",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_StateCurfew_OperatorIniatorId",
                table: "StateCurfew",
                column: "OperatorIniatorId");

            migrationBuilder.CreateIndex(
                name: "IX_StateCurfew_SettlementId",
                table: "StateCurfew",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_StateCurfew_TownId",
                table: "StateCurfew",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_AdminAuthorizerID",
                table: "SecurityTips",
                column: "AdminAuthorizerID");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_ExternalAuthorizerId",
                table: "SecurityTips",
                column: "ExternalAuthorizerId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingItem_VGNGAStaffId",
                table: "MissingItem",
                column: "VGNGAStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserLGA_VGNGALGAStaffId",
                table: "ApplicationUserLGA",
                column: "VGNGALGAStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSettlement_VGNGASettlementStaffId",
                table: "ApplicationUserSettlement",
                column: "VGNGASettlementStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserState_VGNGAStateStaffId",
                table: "ApplicationUserState",
                column: "VGNGAStateStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTown_VGNGATownStaffId",
                table: "ApplicationUserTown",
                column: "VGNGATownStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Users_HodId",
                table: "Departments",
                column: "HodId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Users_SecretaryId",
                table: "Departments",
                column: "SecretaryId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MissingItem_Users_VGNGAStaffId",
                table: "MissingItem",
                column: "VGNGAStaffId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MissingPerson_Users_AdminAuthorizerID",
                table: "MissingPerson",
                column: "AdminAuthorizerID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_Users_AdminAuthorizerID",
                table: "SecurityTips",
                column: "AdminAuthorizerID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_Users_BroadcasterId",
                table: "SecurityTips",
                column: "BroadcasterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_StateCurfew_LGAs_LGAId",
                table: "StateCurfew",
                column: "LGAId",
                principalTable: "LGAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateCurfew_Settlements_SettlementId",
                table: "StateCurfew",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateCurfew_States_StateId",
                table: "StateCurfew",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateCurfew_Towns_TownId",
                table: "StateCurfew",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateCurfew_Users_AdminAuthorizerId",
                table: "StateCurfew",
                column: "AdminAuthorizerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateCurfew_Users_OperatorIniatorId",
                table: "StateCurfew",
                column: "OperatorIniatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_HodId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_SecretaryId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_MissingItem_Users_VGNGAStaffId",
                table: "MissingItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MissingPerson_Users_AdminAuthorizerID",
                table: "MissingPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_Users_AdminAuthorizerID",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_Users_BroadcasterId",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_Users_ExternalAuthorizerId",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_Users_ExternalInitiatorId",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_StateCurfew_LGAs_LGAId",
                table: "StateCurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_StateCurfew_Settlements_SettlementId",
                table: "StateCurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_StateCurfew_States_StateId",
                table: "StateCurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_StateCurfew_Towns_TownId",
                table: "StateCurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_StateCurfew_Users_AdminAuthorizerId",
                table: "StateCurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_StateCurfew_Users_OperatorIniatorId",
                table: "StateCurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ApplicationUserLGA");

            migrationBuilder.DropTable(
                name: "ApplicationUserSettlement");

            migrationBuilder.DropTable(
                name: "ApplicationUserState");

            migrationBuilder.DropTable(
                name: "ApplicationUserTown");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_StateCurfew_LGAId",
                table: "StateCurfew");

            migrationBuilder.DropIndex(
                name: "IX_StateCurfew_OperatorIniatorId",
                table: "StateCurfew");

            migrationBuilder.DropIndex(
                name: "IX_StateCurfew_SettlementId",
                table: "StateCurfew");

            migrationBuilder.DropIndex(
                name: "IX_StateCurfew_TownId",
                table: "StateCurfew");

            migrationBuilder.DropIndex(
                name: "IX_SecurityTips_AdminAuthorizerID",
                table: "SecurityTips");

            migrationBuilder.DropIndex(
                name: "IX_SecurityTips_ExternalAuthorizerId",
                table: "SecurityTips");

            migrationBuilder.DropIndex(
                name: "IX_MissingItem_VGNGAStaffId",
                table: "MissingItem");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExternalStaffType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsAppAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsAppOperator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsAppSuperAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsExternalAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsExternalOperator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsExternalSuperAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SalaryCurrency",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsAuthorized",
                table: "StateCurfew");

            migrationBuilder.DropColumn(
                name: "LGAId",
                table: "StateCurfew");

            migrationBuilder.DropColumn(
                name: "OperatorIniatorId",
                table: "StateCurfew");

            migrationBuilder.DropColumn(
                name: "SettlementId",
                table: "StateCurfew");

            migrationBuilder.DropColumn(
                name: "TownId",
                table: "StateCurfew");

            migrationBuilder.DropColumn(
                name: "ExternalAuthorizerId",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "VGNGAStaffId",
                table: "MissingItem");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Users",
                newName: "ActiveStatus");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Users",
                newName: "LastLocationCoordinates");

            migrationBuilder.RenameColumn(
                name: "AdminAuthorizerId",
                table: "StateCurfew",
                newName: "AdminAuthorizerID");

            migrationBuilder.RenameIndex(
                name: "IX_StateCurfew_AdminAuthorizerId",
                table: "StateCurfew",
                newName: "IX_StateCurfew_AdminAuthorizerID");

            migrationBuilder.RenameColumn(
                name: "ExternalInitiatorId",
                table: "SecurityTips",
                newName: "VGNGAAdminAuthorizerId");

            migrationBuilder.RenameIndex(
                name: "IX_SecurityTips_ExternalInitiatorId",
                table: "SecurityTips",
                newName: "IX_SecurityTips_VGNGAAdminAuthorizerId");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "StateCurfew",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminAuthorizerID",
                table: "StateCurfew",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminAuthorizerID",
                table: "SecurityTips",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TownId",
                table: "MissingItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SettlementId",
                table: "MissingItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminAuthorizerID",
                table: "MissingItem",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "NPFLGAStaff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsOperator = table.Column<bool>(type: "bit", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    LGAId = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPFLGAStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NPFLGAStaff_LGAs_LGAId",
                        column: x => x.LGAId,
                        principalTable: "LGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NPFSettlementAdmin",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsOperator = table.Column<bool>(type: "bit", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettlementId = table.Column<int>(type: "int", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPFSettlementAdmin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NPFSettlementAdmin_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NPFStateStaff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsOperator = table.Column<bool>(type: "bit", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPFStateStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NPFStateStaff_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NPFTownStaff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsOperator = table.Column<bool>(type: "bit", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TownId = table.Column<int>(type: "int", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPFTownStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NPFTownStaff_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteLGAStaff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsOperator = table.Column<bool>(type: "bit", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    LGAId = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialVigilanteLGAStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteLGAStaff_LGAs_LGAId",
                        column: x => x.LGAId,
                        principalTable: "LGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteSettlementStaff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsOperator = table.Column<bool>(type: "bit", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettlementId = table.Column<int>(type: "int", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialVigilanteSettlementStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteSettlementStaff_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteStateStaff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsOperator = table.Column<bool>(type: "bit", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialVigilanteStateStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteStateStaff_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteTownStaff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsOperator = table.Column<bool>(type: "bit", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TownId = table.Column<int>(type: "int", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialVigilanteTownStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteTownStaff_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VGNGAStaff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    SalaryCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VGNGAStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VGNGAStaff_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LGACurfew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminAuthorizerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailyEndHour = table.Column<int>(type: "int", nullable: false),
                    DailyStartHour = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LGAId = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGACurfew", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LGACurfew_LGAs_LGAId",
                        column: x => x.LGAId,
                        principalTable: "LGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LGACurfew_VGNGAStaff_AdminAuthorizerID",
                        column: x => x.AdminAuthorizerID,
                        principalTable: "VGNGAStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LGAVGNGAStaff",
                columns: table => new
                {
                    VGNGALGAOperatorsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VGNGAOperatorLGAsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGAVGNGAStaff", x => new { x.VGNGALGAOperatorsId, x.VGNGAOperatorLGAsId });
                    table.ForeignKey(
                        name: "FK_LGAVGNGAStaff_LGAs_VGNGAOperatorLGAsId",
                        column: x => x.VGNGAOperatorLGAsId,
                        principalTable: "LGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LGAVGNGAStaff_VGNGAStaff_VGNGALGAOperatorsId",
                        column: x => x.VGNGALGAOperatorsId,
                        principalTable: "VGNGAStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LGAVGNGAStaff1",
                columns: table => new
                {
                    VGNGAAdminLGAsId = table.Column<int>(type: "int", nullable: false),
                    VGNGALGAAdminsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGAVGNGAStaff1", x => new { x.VGNGAAdminLGAsId, x.VGNGALGAAdminsId });
                    table.ForeignKey(
                        name: "FK_LGAVGNGAStaff1_LGAs_VGNGAAdminLGAsId",
                        column: x => x.VGNGAAdminLGAsId,
                        principalTable: "LGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LGAVGNGAStaff1_VGNGAStaff_VGNGALGAAdminsId",
                        column: x => x.VGNGALGAAdminsId,
                        principalTable: "VGNGAStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettlementCurfew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminAuthorizerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailyEndHour = table.Column<int>(type: "int", nullable: false),
                    DailyStartHour = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettlementId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementCurfew", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettlementCurfew_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettlementCurfew_VGNGAStaff_AdminAuthorizerID",
                        column: x => x.AdminAuthorizerID,
                        principalTable: "VGNGAStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettlementVGNGAStaff",
                columns: table => new
                {
                    VGNGAAdminSettlementsId = table.Column<int>(type: "int", nullable: false),
                    VGNGASettlementAdminsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementVGNGAStaff", x => new { x.VGNGAAdminSettlementsId, x.VGNGASettlementAdminsId });
                    table.ForeignKey(
                        name: "FK_SettlementVGNGAStaff_Settlements_VGNGAAdminSettlementsId",
                        column: x => x.VGNGAAdminSettlementsId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettlementVGNGAStaff_VGNGAStaff_VGNGASettlementAdminsId",
                        column: x => x.VGNGASettlementAdminsId,
                        principalTable: "VGNGAStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettlementVGNGAStaff1",
                columns: table => new
                {
                    VGNGAOperatorSettlementsId = table.Column<int>(type: "int", nullable: false),
                    VGNGASettlementOperatorsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementVGNGAStaff1", x => new { x.VGNGAOperatorSettlementsId, x.VGNGASettlementOperatorsId });
                    table.ForeignKey(
                        name: "FK_SettlementVGNGAStaff1_Settlements_VGNGAOperatorSettlementsId",
                        column: x => x.VGNGAOperatorSettlementsId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettlementVGNGAStaff1_VGNGAStaff_VGNGASettlementOperatorsId",
                        column: x => x.VGNGASettlementOperatorsId,
                        principalTable: "VGNGAStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateVGNGAStaff",
                columns: table => new
                {
                    VGNGAAdminStatesId = table.Column<int>(type: "int", nullable: false),
                    VGNGAStateAdminsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateVGNGAStaff", x => new { x.VGNGAAdminStatesId, x.VGNGAStateAdminsId });
                    table.ForeignKey(
                        name: "FK_StateVGNGAStaff_States_VGNGAAdminStatesId",
                        column: x => x.VGNGAAdminStatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StateVGNGAStaff_VGNGAStaff_VGNGAStateAdminsId",
                        column: x => x.VGNGAStateAdminsId,
                        principalTable: "VGNGAStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateVGNGAStaff1",
                columns: table => new
                {
                    VGNGAOperatorStatesId = table.Column<int>(type: "int", nullable: false),
                    VGNGAStateOperatorsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateVGNGAStaff1", x => new { x.VGNGAOperatorStatesId, x.VGNGAStateOperatorsId });
                    table.ForeignKey(
                        name: "FK_StateVGNGAStaff1_States_VGNGAOperatorStatesId",
                        column: x => x.VGNGAOperatorStatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StateVGNGAStaff1_VGNGAStaff_VGNGAStateOperatorsId",
                        column: x => x.VGNGAStateOperatorsId,
                        principalTable: "VGNGAStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TownCurfew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminAuthorizerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailyEndHour = table.Column<int>(type: "int", nullable: false),
                    DailyStartHour = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TownId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TownCurfew", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TownCurfew_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TownCurfew_VGNGAStaff_AdminAuthorizerID",
                        column: x => x.AdminAuthorizerID,
                        principalTable: "VGNGAStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TownVGNGAStaff",
                columns: table => new
                {
                    VGNGAAdminTownsId = table.Column<int>(type: "int", nullable: false),
                    VGNGATownAdminsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TownVGNGAStaff", x => new { x.VGNGAAdminTownsId, x.VGNGATownAdminsId });
                    table.ForeignKey(
                        name: "FK_TownVGNGAStaff_Towns_VGNGAAdminTownsId",
                        column: x => x.VGNGAAdminTownsId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TownVGNGAStaff_VGNGAStaff_VGNGATownAdminsId",
                        column: x => x.VGNGATownAdminsId,
                        principalTable: "VGNGAStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TownVGNGAStaff1",
                columns: table => new
                {
                    VGNGAOperatorTownsId = table.Column<int>(type: "int", nullable: false),
                    VGNGATownOperatorsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TownVGNGAStaff1", x => new { x.VGNGAOperatorTownsId, x.VGNGATownOperatorsId });
                    table.ForeignKey(
                        name: "FK_TownVGNGAStaff1_Towns_VGNGAOperatorTownsId",
                        column: x => x.VGNGAOperatorTownsId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TownVGNGAStaff1_VGNGAStaff_VGNGATownOperatorsId",
                        column: x => x.VGNGATownOperatorsId,
                        principalTable: "VGNGAStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissingItem_AdminAuthorizerID",
                table: "MissingItem",
                column: "AdminAuthorizerID");

            migrationBuilder.CreateIndex(
                name: "IX_LGACurfew_AdminAuthorizerID",
                table: "LGACurfew",
                column: "AdminAuthorizerID");

            migrationBuilder.CreateIndex(
                name: "IX_LGACurfew_LGAId",
                table: "LGACurfew",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_LGAVGNGAStaff_VGNGAOperatorLGAsId",
                table: "LGAVGNGAStaff",
                column: "VGNGAOperatorLGAsId");

            migrationBuilder.CreateIndex(
                name: "IX_LGAVGNGAStaff1_VGNGALGAAdminsId",
                table: "LGAVGNGAStaff1",
                column: "VGNGALGAAdminsId");

            migrationBuilder.CreateIndex(
                name: "IX_NPFLGAStaff_LGAId",
                table: "NPFLGAStaff",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_NPFSettlementAdmin_SettlementId",
                table: "NPFSettlementAdmin",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_NPFStateStaff_StateId",
                table: "NPFStateStaff",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_NPFTownStaff_TownId",
                table: "NPFTownStaff",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialVigilanteLGAStaff_LGAId",
                table: "OfficialVigilanteLGAStaff",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialVigilanteSettlementStaff_SettlementId",
                table: "OfficialVigilanteSettlementStaff",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialVigilanteStateStaff_StateId",
                table: "OfficialVigilanteStateStaff",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialVigilanteTownStaff_TownId",
                table: "OfficialVigilanteTownStaff",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementCurfew_AdminAuthorizerID",
                table: "SettlementCurfew",
                column: "AdminAuthorizerID");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementCurfew_SettlementId",
                table: "SettlementCurfew",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementVGNGAStaff_VGNGASettlementAdminsId",
                table: "SettlementVGNGAStaff",
                column: "VGNGASettlementAdminsId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementVGNGAStaff1_VGNGASettlementOperatorsId",
                table: "SettlementVGNGAStaff1",
                column: "VGNGASettlementOperatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_StateVGNGAStaff_VGNGAStateAdminsId",
                table: "StateVGNGAStaff",
                column: "VGNGAStateAdminsId");

            migrationBuilder.CreateIndex(
                name: "IX_StateVGNGAStaff1_VGNGAStateOperatorsId",
                table: "StateVGNGAStaff1",
                column: "VGNGAStateOperatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_TownCurfew_AdminAuthorizerID",
                table: "TownCurfew",
                column: "AdminAuthorizerID");

            migrationBuilder.CreateIndex(
                name: "IX_TownCurfew_TownId",
                table: "TownCurfew",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_TownVGNGAStaff_VGNGATownAdminsId",
                table: "TownVGNGAStaff",
                column: "VGNGATownAdminsId");

            migrationBuilder.CreateIndex(
                name: "IX_TownVGNGAStaff1_VGNGATownOperatorsId",
                table: "TownVGNGAStaff1",
                column: "VGNGATownOperatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_VGNGAStaff_DepartmentId",
                table: "VGNGAStaff",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_VGNGAStaff_HodId",
                table: "Departments",
                column: "HodId",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_VGNGAStaff_SecretaryId",
                table: "Departments",
                column: "SecretaryId",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MissingItem_VGNGAStaff_AdminAuthorizerID",
                table: "MissingItem",
                column: "AdminAuthorizerID",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MissingPerson_VGNGAStaff_AdminAuthorizerID",
                table: "MissingPerson",
                column: "AdminAuthorizerID",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_Users_BroadcasterId",
                table: "SecurityTips",
                column: "BroadcasterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_VGNGAStaff_VGNGAAdminAuthorizerId",
                table: "SecurityTips",
                column: "VGNGAAdminAuthorizerId",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateCurfew_States_StateId",
                table: "StateCurfew",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StateCurfew_VGNGAStaff_AdminAuthorizerID",
                table: "StateCurfew",
                column: "AdminAuthorizerID",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
