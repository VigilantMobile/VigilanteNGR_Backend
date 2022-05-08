using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Persistence.Migrations
{
    public partial class _generic_staff_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LGANPFLGAAdmin");

            migrationBuilder.DropTable(
                name: "LGANPFLGAOperator");

            migrationBuilder.DropTable(
                name: "LGAOfficialVigilanteLGAAdmin");

            migrationBuilder.DropTable(
                name: "LGAOfficialVigilanteLGAOperator");

            migrationBuilder.DropTable(
                name: "NPFSettlementAdminSettlement");

            migrationBuilder.DropTable(
                name: "NPFSettlementOperatorSettlement");

            migrationBuilder.DropTable(
                name: "NPFStateAdminState");

            migrationBuilder.DropTable(
                name: "NPFStateOperatorState");

            migrationBuilder.DropTable(
                name: "NPFTownAdminTown");

            migrationBuilder.DropTable(
                name: "NPFTownOperatorTown");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteSettlementAdminSettlement");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteSettlementOperatorSettlement");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteStateAdminState");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteStateOperatorState");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteTownAdminTown");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteTownOperatorTown");

            migrationBuilder.DropTable(
                name: "NPFLGAOperator");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteLGAOperators");

            migrationBuilder.DropTable(
                name: "NPFSettlementOperator");

            migrationBuilder.DropTable(
                name: "NPFStateOperator");

            migrationBuilder.DropTable(
                name: "NPFTownOperator");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteSettlementOperators");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteStateOperators");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsSuperAdmin",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "VGNGAStaff",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VGNGAStaff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "VGNGAStaff",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "VGNGAStaff",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "VGNGAStaff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "VGNGAStaff",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "VGNGAStaff",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "OfficialVigilanteStateAdmins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "OfficialVigilanteStateAdmins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "OfficialVigilanteStateAdmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OfficialVigilanteStateAdmins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOperator",
                table: "OfficialVigilanteStateAdmins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "OfficialVigilanteStateAdmins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "OfficialVigilanteStateAdmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "OfficialVigilanteStateAdmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "OfficialVigilanteStateAdmins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "OfficialVigilanteStateAdmins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "OfficialVigilanteSettlementAdmins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "OfficialVigilanteSettlementAdmins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "OfficialVigilanteSettlementAdmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OfficialVigilanteSettlementAdmins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOperator",
                table: "OfficialVigilanteSettlementAdmins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "OfficialVigilanteSettlementAdmins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "OfficialVigilanteSettlementAdmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "OfficialVigilanteSettlementAdmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "OfficialVigilanteSettlementAdmins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SettlementId",
                table: "OfficialVigilanteSettlementAdmins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "OfficialVigilanteLGAAdmins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "OfficialVigilanteLGAAdmins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "OfficialVigilanteLGAAdmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OfficialVigilanteLGAAdmins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOperator",
                table: "OfficialVigilanteLGAAdmins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LGAId",
                table: "OfficialVigilanteLGAAdmins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "OfficialVigilanteLGAAdmins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "OfficialVigilanteLGAAdmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "OfficialVigilanteLGAAdmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "OfficialVigilanteLGAAdmins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "NPFTownAdmin",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "NPFTownAdmin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "NPFTownAdmin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "NPFTownAdmin",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOperator",
                table: "NPFTownAdmin",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "NPFTownAdmin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "NPFTownAdmin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "NPFTownAdmin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "NPFTownAdmin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TownId",
                table: "NPFTownAdmin",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "NPFStateAdmins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "NPFStateAdmins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "NPFStateAdmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "NPFStateAdmins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOperator",
                table: "NPFStateAdmins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "NPFStateAdmins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "NPFStateAdmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "NPFStateAdmins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "NPFStateAdmins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "NPFStateAdmins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "NPFSettlementAdmin",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "NPFSettlementAdmin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "NPFSettlementAdmin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "NPFSettlementAdmin",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOperator",
                table: "NPFSettlementAdmin",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "NPFSettlementAdmin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "NPFSettlementAdmin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "NPFSettlementAdmin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "NPFSettlementAdmin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SettlementId",
                table: "NPFSettlementAdmin",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "NPFLGAAdmin",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "NPFLGAAdmin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "NPFLGAAdmin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "NPFLGAAdmin",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOperator",
                table: "NPFLGAAdmin",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LGAId",
                table: "NPFLGAAdmin",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "NPFLGAAdmin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "NPFLGAAdmin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "NPFLGAAdmin",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "NPFLGAAdmin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteTownAdmins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    IsOperator = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TownId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialVigilanteTownAdmins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteTownAdmins_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfficialVigilanteStateAdmins_StateId",
                table: "OfficialVigilanteStateAdmins",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialVigilanteSettlementAdmins_SettlementId",
                table: "OfficialVigilanteSettlementAdmins",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialVigilanteLGAAdmins_LGAId",
                table: "OfficialVigilanteLGAAdmins",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_NPFTownAdmin_TownId",
                table: "NPFTownAdmin",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_NPFStateAdmins_StateId",
                table: "NPFStateAdmins",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_NPFSettlementAdmin_SettlementId",
                table: "NPFSettlementAdmin",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_NPFLGAAdmin_LGAId",
                table: "NPFLGAAdmin",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialVigilanteTownAdmins_TownId",
                table: "OfficialVigilanteTownAdmins",
                column: "TownId");

            migrationBuilder.AddForeignKey(
                name: "FK_NPFLGAAdmin_LGAs_LGAId",
                table: "NPFLGAAdmin",
                column: "LGAId",
                principalTable: "LGAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFSettlementAdmin_Settlements_SettlementId",
                table: "NPFSettlementAdmin",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFStateAdmins_States_StateId",
                table: "NPFStateAdmins",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFTownAdmin_Towns_TownId",
                table: "NPFTownAdmin",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteLGAAdmins_LGAs_LGAId",
                table: "OfficialVigilanteLGAAdmins",
                column: "LGAId",
                principalTable: "LGAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteSettlementAdmins_Settlements_SettlementId",
                table: "OfficialVigilanteSettlementAdmins",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteStateAdmins_States_StateId",
                table: "OfficialVigilanteStateAdmins",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NPFLGAAdmin_LGAs_LGAId",
                table: "NPFLGAAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFSettlementAdmin_Settlements_SettlementId",
                table: "NPFSettlementAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFStateAdmins_States_StateId",
                table: "NPFStateAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFTownAdmin_Towns_TownId",
                table: "NPFTownAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteLGAAdmins_LGAs_LGAId",
                table: "OfficialVigilanteLGAAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteSettlementAdmins_Settlements_SettlementId",
                table: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteStateAdmins_States_StateId",
                table: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteTownAdmins");

            migrationBuilder.DropIndex(
                name: "IX_OfficialVigilanteStateAdmins_StateId",
                table: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropIndex(
                name: "IX_OfficialVigilanteSettlementAdmins_SettlementId",
                table: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropIndex(
                name: "IX_OfficialVigilanteLGAAdmins_LGAId",
                table: "OfficialVigilanteLGAAdmins");

            migrationBuilder.DropIndex(
                name: "IX_NPFTownAdmin_TownId",
                table: "NPFTownAdmin");

            migrationBuilder.DropIndex(
                name: "IX_NPFStateAdmins_StateId",
                table: "NPFStateAdmins");

            migrationBuilder.DropIndex(
                name: "IX_NPFSettlementAdmin_SettlementId",
                table: "NPFSettlementAdmin");

            migrationBuilder.DropIndex(
                name: "IX_NPFLGAAdmin_LGAId",
                table: "NPFLGAAdmin");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "VGNGAStaff");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VGNGAStaff");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "VGNGAStaff");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "VGNGAStaff");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "VGNGAStaff");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "VGNGAStaff");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "VGNGAStaff");

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropColumn(
                name: "IsOperator",
                table: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropColumn(
                name: "IsOperator",
                table: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropColumn(
                name: "SettlementId",
                table: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "OfficialVigilanteLGAAdmins");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "OfficialVigilanteLGAAdmins");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "OfficialVigilanteLGAAdmins");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OfficialVigilanteLGAAdmins");

            migrationBuilder.DropColumn(
                name: "IsOperator",
                table: "OfficialVigilanteLGAAdmins");

            migrationBuilder.DropColumn(
                name: "LGAId",
                table: "OfficialVigilanteLGAAdmins");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "OfficialVigilanteLGAAdmins");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "OfficialVigilanteLGAAdmins");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "OfficialVigilanteLGAAdmins");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "OfficialVigilanteLGAAdmins");

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "NPFTownAdmin");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "NPFTownAdmin");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "NPFTownAdmin");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "NPFTownAdmin");

            migrationBuilder.DropColumn(
                name: "IsOperator",
                table: "NPFTownAdmin");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "NPFTownAdmin");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "NPFTownAdmin");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "NPFTownAdmin");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "NPFTownAdmin");

            migrationBuilder.DropColumn(
                name: "TownId",
                table: "NPFTownAdmin");

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "NPFStateAdmins");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "NPFStateAdmins");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "NPFStateAdmins");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "NPFStateAdmins");

            migrationBuilder.DropColumn(
                name: "IsOperator",
                table: "NPFStateAdmins");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "NPFStateAdmins");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "NPFStateAdmins");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "NPFStateAdmins");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "NPFStateAdmins");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "NPFStateAdmins");

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "NPFSettlementAdmin");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "NPFSettlementAdmin");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "NPFSettlementAdmin");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "NPFSettlementAdmin");

            migrationBuilder.DropColumn(
                name: "IsOperator",
                table: "NPFSettlementAdmin");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "NPFSettlementAdmin");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "NPFSettlementAdmin");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "NPFSettlementAdmin");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "NPFSettlementAdmin");

            migrationBuilder.DropColumn(
                name: "SettlementId",
                table: "NPFSettlementAdmin");

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "NPFLGAAdmin");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "NPFLGAAdmin");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "NPFLGAAdmin");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "NPFLGAAdmin");

            migrationBuilder.DropColumn(
                name: "IsOperator",
                table: "NPFLGAAdmin");

            migrationBuilder.DropColumn(
                name: "LGAId",
                table: "NPFLGAAdmin");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "NPFLGAAdmin");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "NPFLGAAdmin");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "NPFLGAAdmin");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "NPFLGAAdmin");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsSuperAdmin",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LGANPFLGAAdmin",
                columns: table => new
                {
                    LGAsId = table.Column<int>(type: "int", nullable: false),
                    NPFLGAAdminsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGANPFLGAAdmin", x => new { x.LGAsId, x.NPFLGAAdminsId });
                    table.ForeignKey(
                        name: "FK_LGANPFLGAAdmin_LGAs_LGAsId",
                        column: x => x.LGAsId,
                        principalTable: "LGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LGANPFLGAAdmin_NPFLGAAdmin_NPFLGAAdminsId",
                        column: x => x.NPFLGAAdminsId,
                        principalTable: "NPFLGAAdmin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LGAOfficialVigilanteLGAAdmin",
                columns: table => new
                {
                    LGAsId = table.Column<int>(type: "int", nullable: false),
                    OfficialVigilanteLGAAdminsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGAOfficialVigilanteLGAAdmin", x => new { x.LGAsId, x.OfficialVigilanteLGAAdminsId });
                    table.ForeignKey(
                        name: "FK_LGAOfficialVigilanteLGAAdmin_LGAs_LGAsId",
                        column: x => x.LGAsId,
                        principalTable: "LGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LGAOfficialVigilanteLGAAdmin_OfficialVigilanteLGAAdmins_OfficialVigilanteLGAAdminsId",
                        column: x => x.OfficialVigilanteLGAAdminsId,
                        principalTable: "OfficialVigilanteLGAAdmins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NPFLGAOperator",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPFLGAOperator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NPFSettlementAdminSettlement",
                columns: table => new
                {
                    NPFSettlementAdminsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SettlementsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPFSettlementAdminSettlement", x => new { x.NPFSettlementAdminsId, x.SettlementsId });
                    table.ForeignKey(
                        name: "FK_NPFSettlementAdminSettlement_NPFSettlementAdmin_NPFSettlementAdminsId",
                        column: x => x.NPFSettlementAdminsId,
                        principalTable: "NPFSettlementAdmin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPFSettlementAdminSettlement_Settlements_SettlementsId",
                        column: x => x.SettlementsId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NPFSettlementOperator",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPFSettlementOperator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NPFStateAdminState",
                columns: table => new
                {
                    NPFStateAdminsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPFStateAdminState", x => new { x.NPFStateAdminsId, x.StatesId });
                    table.ForeignKey(
                        name: "FK_NPFStateAdminState_NPFStateAdmins_NPFStateAdminsId",
                        column: x => x.NPFStateAdminsId,
                        principalTable: "NPFStateAdmins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPFStateAdminState_States_StatesId",
                        column: x => x.StatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NPFStateOperator",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPFStateOperator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NPFTownAdminTown",
                columns: table => new
                {
                    NPFTownAdminsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TownsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPFTownAdminTown", x => new { x.NPFTownAdminsId, x.TownsId });
                    table.ForeignKey(
                        name: "FK_NPFTownAdminTown_NPFTownAdmin_NPFTownAdminsId",
                        column: x => x.NPFTownAdminsId,
                        principalTable: "NPFTownAdmin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPFTownAdminTown_Towns_TownsId",
                        column: x => x.TownsId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NPFTownOperator",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPFTownOperator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteLGAOperators",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialVigilanteLGAOperators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteSettlementAdminSettlement",
                columns: table => new
                {
                    OfficialVigilanteSettlementAdminsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SettlementsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialVigilanteSettlementAdminSettlement", x => new { x.OfficialVigilanteSettlementAdminsId, x.SettlementsId });
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteSettlementAdminSettlement_OfficialVigilanteSettlementAdmins_OfficialVigilanteSettlementAdminsId",
                        column: x => x.OfficialVigilanteSettlementAdminsId,
                        principalTable: "OfficialVigilanteSettlementAdmins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteSettlementAdminSettlement_Settlements_SettlementsId",
                        column: x => x.SettlementsId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteSettlementOperators",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialVigilanteSettlementOperators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteStateAdminState",
                columns: table => new
                {
                    OfficialVigilanteStateAdminsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialVigilanteStateAdminState", x => new { x.OfficialVigilanteStateAdminsId, x.StatesId });
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteStateAdminState_OfficialVigilanteStateAdmins_OfficialVigilanteStateAdminsId",
                        column: x => x.OfficialVigilanteStateAdminsId,
                        principalTable: "OfficialVigilanteStateAdmins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteStateAdminState_States_StatesId",
                        column: x => x.StatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteStateOperators",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialVigilanteStateOperators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteTownAdminTown",
                columns: table => new
                {
                    OfficialVigilanteTownAdminsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TownsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialVigilanteTownAdminTown", x => new { x.OfficialVigilanteTownAdminsId, x.TownsId });
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteTownAdminTown_Towns_TownsId",
                        column: x => x.TownsId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteTownAdminTown_Users_OfficialVigilanteTownAdminsId",
                        column: x => x.OfficialVigilanteTownAdminsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteTownOperatorTown",
                columns: table => new
                {
                    OfficialVigilanteTownOperatorsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TownsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialVigilanteTownOperatorTown", x => new { x.OfficialVigilanteTownOperatorsId, x.TownsId });
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteTownOperatorTown_Towns_TownsId",
                        column: x => x.TownsId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteTownOperatorTown_Users_OfficialVigilanteTownOperatorsId",
                        column: x => x.OfficialVigilanteTownOperatorsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LGANPFLGAOperator",
                columns: table => new
                {
                    LGAsId = table.Column<int>(type: "int", nullable: false),
                    NPFLGAOperatorsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGANPFLGAOperator", x => new { x.LGAsId, x.NPFLGAOperatorsId });
                    table.ForeignKey(
                        name: "FK_LGANPFLGAOperator_LGAs_LGAsId",
                        column: x => x.LGAsId,
                        principalTable: "LGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LGANPFLGAOperator_NPFLGAOperator_NPFLGAOperatorsId",
                        column: x => x.NPFLGAOperatorsId,
                        principalTable: "NPFLGAOperator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NPFSettlementOperatorSettlement",
                columns: table => new
                {
                    NPFSettlementOperatorsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SettlementsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPFSettlementOperatorSettlement", x => new { x.NPFSettlementOperatorsId, x.SettlementsId });
                    table.ForeignKey(
                        name: "FK_NPFSettlementOperatorSettlement_NPFSettlementOperator_NPFSettlementOperatorsId",
                        column: x => x.NPFSettlementOperatorsId,
                        principalTable: "NPFSettlementOperator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPFSettlementOperatorSettlement_Settlements_SettlementsId",
                        column: x => x.SettlementsId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NPFStateOperatorState",
                columns: table => new
                {
                    NPFStateOperatorsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPFStateOperatorState", x => new { x.NPFStateOperatorsId, x.StatesId });
                    table.ForeignKey(
                        name: "FK_NPFStateOperatorState_NPFStateOperator_NPFStateOperatorsId",
                        column: x => x.NPFStateOperatorsId,
                        principalTable: "NPFStateOperator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPFStateOperatorState_States_StatesId",
                        column: x => x.StatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NPFTownOperatorTown",
                columns: table => new
                {
                    NPFTownOperatorsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TownsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NPFTownOperatorTown", x => new { x.NPFTownOperatorsId, x.TownsId });
                    table.ForeignKey(
                        name: "FK_NPFTownOperatorTown_NPFTownOperator_NPFTownOperatorsId",
                        column: x => x.NPFTownOperatorsId,
                        principalTable: "NPFTownOperator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPFTownOperatorTown_Towns_TownsId",
                        column: x => x.TownsId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LGAOfficialVigilanteLGAOperator",
                columns: table => new
                {
                    LGAsId = table.Column<int>(type: "int", nullable: false),
                    OfficialVigilanteLGAOperatorsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGAOfficialVigilanteLGAOperator", x => new { x.LGAsId, x.OfficialVigilanteLGAOperatorsId });
                    table.ForeignKey(
                        name: "FK_LGAOfficialVigilanteLGAOperator_LGAs_LGAsId",
                        column: x => x.LGAsId,
                        principalTable: "LGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LGAOfficialVigilanteLGAOperator_OfficialVigilanteLGAOperators_OfficialVigilanteLGAOperatorsId",
                        column: x => x.OfficialVigilanteLGAOperatorsId,
                        principalTable: "OfficialVigilanteLGAOperators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteSettlementOperatorSettlement",
                columns: table => new
                {
                    OfficialVigilanteSettlementOperatorsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SettlementsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialVigilanteSettlementOperatorSettlement", x => new { x.OfficialVigilanteSettlementOperatorsId, x.SettlementsId });
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteSettlementOperatorSettlement_OfficialVigilanteSettlementOperators_OfficialVigilanteSettlementOperatorsId",
                        column: x => x.OfficialVigilanteSettlementOperatorsId,
                        principalTable: "OfficialVigilanteSettlementOperators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteSettlementOperatorSettlement_Settlements_SettlementsId",
                        column: x => x.SettlementsId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteStateOperatorState",
                columns: table => new
                {
                    OfficialVigilanteStateOperatorsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialVigilanteStateOperatorState", x => new { x.OfficialVigilanteStateOperatorsId, x.StatesId });
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteStateOperatorState_OfficialVigilanteStateOperators_OfficialVigilanteStateOperatorsId",
                        column: x => x.OfficialVigilanteStateOperatorsId,
                        principalTable: "OfficialVigilanteStateOperators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteStateOperatorState_States_StatesId",
                        column: x => x.StatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LGANPFLGAAdmin_NPFLGAAdminsId",
                table: "LGANPFLGAAdmin",
                column: "NPFLGAAdminsId");

            migrationBuilder.CreateIndex(
                name: "IX_LGANPFLGAOperator_NPFLGAOperatorsId",
                table: "LGANPFLGAOperator",
                column: "NPFLGAOperatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_LGAOfficialVigilanteLGAAdmin_OfficialVigilanteLGAAdminsId",
                table: "LGAOfficialVigilanteLGAAdmin",
                column: "OfficialVigilanteLGAAdminsId");

            migrationBuilder.CreateIndex(
                name: "IX_LGAOfficialVigilanteLGAOperator_OfficialVigilanteLGAOperatorsId",
                table: "LGAOfficialVigilanteLGAOperator",
                column: "OfficialVigilanteLGAOperatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_NPFSettlementAdminSettlement_SettlementsId",
                table: "NPFSettlementAdminSettlement",
                column: "SettlementsId");

            migrationBuilder.CreateIndex(
                name: "IX_NPFSettlementOperatorSettlement_SettlementsId",
                table: "NPFSettlementOperatorSettlement",
                column: "SettlementsId");

            migrationBuilder.CreateIndex(
                name: "IX_NPFStateAdminState_StatesId",
                table: "NPFStateAdminState",
                column: "StatesId");

            migrationBuilder.CreateIndex(
                name: "IX_NPFStateOperatorState_StatesId",
                table: "NPFStateOperatorState",
                column: "StatesId");

            migrationBuilder.CreateIndex(
                name: "IX_NPFTownAdminTown_TownsId",
                table: "NPFTownAdminTown",
                column: "TownsId");

            migrationBuilder.CreateIndex(
                name: "IX_NPFTownOperatorTown_TownsId",
                table: "NPFTownOperatorTown",
                column: "TownsId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialVigilanteSettlementAdminSettlement_SettlementsId",
                table: "OfficialVigilanteSettlementAdminSettlement",
                column: "SettlementsId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialVigilanteSettlementOperatorSettlement_SettlementsId",
                table: "OfficialVigilanteSettlementOperatorSettlement",
                column: "SettlementsId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialVigilanteStateAdminState_StatesId",
                table: "OfficialVigilanteStateAdminState",
                column: "StatesId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialVigilanteStateOperatorState_StatesId",
                table: "OfficialVigilanteStateOperatorState",
                column: "StatesId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialVigilanteTownAdminTown_TownsId",
                table: "OfficialVigilanteTownAdminTown",
                column: "TownsId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialVigilanteTownOperatorTown_TownsId",
                table: "OfficialVigilanteTownOperatorTown",
                column: "TownsId");
        }
    }
}
