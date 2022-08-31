using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Persistence.Migrations
{
    public partial class location_identity_entities_refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_HodId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_SecretaryId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_LGACurfew_Users_AdminAuthorizerID",
                table: "LGACurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_LGANPFLGAAdmin_Users_NPFLGAAdminsId",
                table: "LGANPFLGAAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_LGANPFLGAOperator_Users_NPFLGAOperatorsId",
                table: "LGANPFLGAOperator");

            migrationBuilder.DropForeignKey(
                name: "FK_LGAOfficialVigilanteLGAAdmin_Users_OfficialVigilanteLGAAdminsId",
                table: "LGAOfficialVigilanteLGAAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_LGAOfficialVigilanteLGAOperator_Users_OfficialVigilanteLGAOperatorsId",
                table: "LGAOfficialVigilanteLGAOperator");

            migrationBuilder.DropForeignKey(
                name: "FK_LGAVGNGAStaff_Users_VGNGALGAOperatorsId",
                table: "LGAVGNGAStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_LGAVGNGAStaff1_Users_VGNGALGAAdminsId",
                table: "LGAVGNGAStaff1");

            migrationBuilder.DropForeignKey(
                name: "FK_MissingItem_Users_AdminAuthorizerID",
                table: "MissingItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MissingPerson_Users_AdminAuthorizerID",
                table: "MissingPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFSettlementAdminSettlement_Users_NPFSettlementAdminsId",
                table: "NPFSettlementAdminSettlement");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFSettlementOperatorSettlement_Users_NPFSettlementOperatorsId",
                table: "NPFSettlementOperatorSettlement");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFStateAdminState_Users_NPFStateAdminsId",
                table: "NPFStateAdminState");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFStateOperatorState_Users_NPFStateOperatorsId",
                table: "NPFStateOperatorState");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFTownAdminTown_Users_NPFTownAdminsId",
                table: "NPFTownAdminTown");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFTownOperatorTown_Users_NPFTownOperatorsId",
                table: "NPFTownOperatorTown");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteSettlementAdminSettlement_Users_OfficialVigilanteSettlementAdminsId",
                table: "OfficialVigilanteSettlementAdminSettlement");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteSettlementOperatorSettlement_Users_OfficialVigilanteSettlementOperatorsId",
                table: "OfficialVigilanteSettlementOperatorSettlement");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteStateAdminState_Users_OfficialVigilanteStateAdminsId",
                table: "OfficialVigilanteStateAdminState");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteStateOperatorState_Users_OfficialVigilanteStateOperatorsId",
                table: "OfficialVigilanteStateOperatorState");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_Users_VGNGAAdminAuthorizerId",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SettlementCurfew_Users_AdminAuthorizerID",
                table: "SettlementCurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_SettlementVGNGAStaff_Users_VGNGASettlementAdminsId",
                table: "SettlementVGNGAStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_SettlementVGNGAStaff1_Users_VGNGASettlementOperatorsId",
                table: "SettlementVGNGAStaff1");

            migrationBuilder.DropForeignKey(
                name: "FK_StateCurfew_Users_AdminAuthorizerID",
                table: "StateCurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_StateVGNGAStaff_Users_VGNGAStateAdminsId",
                table: "StateVGNGAStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_StateVGNGAStaff1_Users_VGNGAStateOperatorsId",
                table: "StateVGNGAStaff1");

            migrationBuilder.DropForeignKey(
                name: "FK_TownCurfew_Users_AdminAuthorizerID",
                table: "TownCurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_TownVGNGAStaff_Users_VGNGATownAdminsId",
                table: "TownVGNGAStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_TownVGNGAStaff1_Users_VGNGATownOperatorsId",
                table: "TownVGNGAStaff1");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NPFLGAAdmin_IsSuperAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NPFSettlementAdmin_IsSuperAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NPFStateAdmin_IsSuperAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NPFTownAdmin_IsSuperAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OfficialVigilanteSettlementAdmin_IsSuperAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OfficialVigilanteStateAdmin_IsSuperAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OfficialVigilanteTownAdmin_IsSuperAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SalaryCurrency",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "NPFLGAAdmin",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_NPFLGAAdmin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NPFLGAOperator",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_NPFLGAOperator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NPFSettlementAdmin",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_NPFSettlementAdmin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NPFSettlementOperator",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_NPFSettlementOperator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NPFStateAdmins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_NPFStateAdmins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NPFStateOperator",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_NPFStateOperator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NPFTownAdmin",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_NPFTownAdmin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NPFTownOperator",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_NPFTownOperator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteLGAAdmins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_OfficialVigilanteLGAAdmins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteLGAOperators",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_OfficialVigilanteLGAOperators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteSettlementAdmins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_OfficialVigilanteSettlementAdmins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteSettlementOperators",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_OfficialVigilanteSettlementOperators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteStateAdmins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_OfficialVigilanteStateAdmins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficialVigilanteStateOperators",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_OfficialVigilanteStateOperators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VGNGAStaff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    SalaryCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_VGNGAStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VGNGAStaff_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "FK_LGACurfew_VGNGAStaff_AdminAuthorizerID",
                table: "LGACurfew",
                column: "AdminAuthorizerID",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LGANPFLGAAdmin_NPFLGAAdmin_NPFLGAAdminsId",
                table: "LGANPFLGAAdmin",
                column: "NPFLGAAdminsId",
                principalTable: "NPFLGAAdmin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LGANPFLGAOperator_NPFLGAOperator_NPFLGAOperatorsId",
                table: "LGANPFLGAOperator",
                column: "NPFLGAOperatorsId",
                principalTable: "NPFLGAOperator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LGAOfficialVigilanteLGAAdmin_OfficialVigilanteLGAAdmins_OfficialVigilanteLGAAdminsId",
                table: "LGAOfficialVigilanteLGAAdmin",
                column: "OfficialVigilanteLGAAdminsId",
                principalTable: "OfficialVigilanteLGAAdmins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LGAOfficialVigilanteLGAOperator_OfficialVigilanteLGAOperators_OfficialVigilanteLGAOperatorsId",
                table: "LGAOfficialVigilanteLGAOperator",
                column: "OfficialVigilanteLGAOperatorsId",
                principalTable: "OfficialVigilanteLGAOperators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LGAVGNGAStaff_VGNGAStaff_VGNGALGAOperatorsId",
                table: "LGAVGNGAStaff",
                column: "VGNGALGAOperatorsId",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LGAVGNGAStaff1_VGNGAStaff_VGNGALGAAdminsId",
                table: "LGAVGNGAStaff1",
                column: "VGNGALGAAdminsId",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_NPFSettlementAdminSettlement_NPFSettlementAdmin_NPFSettlementAdminsId",
                table: "NPFSettlementAdminSettlement",
                column: "NPFSettlementAdminsId",
                principalTable: "NPFSettlementAdmin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFSettlementOperatorSettlement_NPFSettlementOperator_NPFSettlementOperatorsId",
                table: "NPFSettlementOperatorSettlement",
                column: "NPFSettlementOperatorsId",
                principalTable: "NPFSettlementOperator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFStateAdminState_NPFStateAdmins_NPFStateAdminsId",
                table: "NPFStateAdminState",
                column: "NPFStateAdminsId",
                principalTable: "NPFStateAdmins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFStateOperatorState_NPFStateOperator_NPFStateOperatorsId",
                table: "NPFStateOperatorState",
                column: "NPFStateOperatorsId",
                principalTable: "NPFStateOperator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFTownAdminTown_NPFTownAdmin_NPFTownAdminsId",
                table: "NPFTownAdminTown",
                column: "NPFTownAdminsId",
                principalTable: "NPFTownAdmin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFTownOperatorTown_NPFTownOperator_NPFTownOperatorsId",
                table: "NPFTownOperatorTown",
                column: "NPFTownOperatorsId",
                principalTable: "NPFTownOperator",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteSettlementAdminSettlement_OfficialVigilanteSettlementAdmins_OfficialVigilanteSettlementAdminsId",
                table: "OfficialVigilanteSettlementAdminSettlement",
                column: "OfficialVigilanteSettlementAdminsId",
                principalTable: "OfficialVigilanteSettlementAdmins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteSettlementOperatorSettlement_OfficialVigilanteSettlementOperators_OfficialVigilanteSettlementOperatorsId",
                table: "OfficialVigilanteSettlementOperatorSettlement",
                column: "OfficialVigilanteSettlementOperatorsId",
                principalTable: "OfficialVigilanteSettlementOperators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteStateAdminState_OfficialVigilanteStateAdmins_OfficialVigilanteStateAdminsId",
                table: "OfficialVigilanteStateAdminState",
                column: "OfficialVigilanteStateAdminsId",
                principalTable: "OfficialVigilanteStateAdmins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteStateOperatorState_OfficialVigilanteStateOperators_OfficialVigilanteStateOperatorsId",
                table: "OfficialVigilanteStateOperatorState",
                column: "OfficialVigilanteStateOperatorsId",
                principalTable: "OfficialVigilanteStateOperators",
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
                name: "FK_SettlementCurfew_VGNGAStaff_AdminAuthorizerID",
                table: "SettlementCurfew",
                column: "AdminAuthorizerID",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SettlementVGNGAStaff_VGNGAStaff_VGNGASettlementAdminsId",
                table: "SettlementVGNGAStaff",
                column: "VGNGASettlementAdminsId",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SettlementVGNGAStaff1_VGNGAStaff_VGNGASettlementOperatorsId",
                table: "SettlementVGNGAStaff1",
                column: "VGNGASettlementOperatorsId",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StateCurfew_VGNGAStaff_AdminAuthorizerID",
                table: "StateCurfew",
                column: "AdminAuthorizerID",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StateVGNGAStaff_VGNGAStaff_VGNGAStateAdminsId",
                table: "StateVGNGAStaff",
                column: "VGNGAStateAdminsId",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StateVGNGAStaff1_VGNGAStaff_VGNGAStateOperatorsId",
                table: "StateVGNGAStaff1",
                column: "VGNGAStateOperatorsId",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TownCurfew_VGNGAStaff_AdminAuthorizerID",
                table: "TownCurfew",
                column: "AdminAuthorizerID",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TownVGNGAStaff_VGNGAStaff_VGNGATownAdminsId",
                table: "TownVGNGAStaff",
                column: "VGNGATownAdminsId",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TownVGNGAStaff1_VGNGAStaff_VGNGATownOperatorsId",
                table: "TownVGNGAStaff1",
                column: "VGNGATownOperatorsId",
                principalTable: "VGNGAStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_VGNGAStaff_HodId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_VGNGAStaff_SecretaryId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_LGACurfew_VGNGAStaff_AdminAuthorizerID",
                table: "LGACurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_LGANPFLGAAdmin_NPFLGAAdmin_NPFLGAAdminsId",
                table: "LGANPFLGAAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_LGANPFLGAOperator_NPFLGAOperator_NPFLGAOperatorsId",
                table: "LGANPFLGAOperator");

            migrationBuilder.DropForeignKey(
                name: "FK_LGAOfficialVigilanteLGAAdmin_OfficialVigilanteLGAAdmins_OfficialVigilanteLGAAdminsId",
                table: "LGAOfficialVigilanteLGAAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_LGAOfficialVigilanteLGAOperator_OfficialVigilanteLGAOperators_OfficialVigilanteLGAOperatorsId",
                table: "LGAOfficialVigilanteLGAOperator");

            migrationBuilder.DropForeignKey(
                name: "FK_LGAVGNGAStaff_VGNGAStaff_VGNGALGAOperatorsId",
                table: "LGAVGNGAStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_LGAVGNGAStaff1_VGNGAStaff_VGNGALGAAdminsId",
                table: "LGAVGNGAStaff1");

            migrationBuilder.DropForeignKey(
                name: "FK_MissingItem_VGNGAStaff_AdminAuthorizerID",
                table: "MissingItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MissingPerson_VGNGAStaff_AdminAuthorizerID",
                table: "MissingPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFSettlementAdminSettlement_NPFSettlementAdmin_NPFSettlementAdminsId",
                table: "NPFSettlementAdminSettlement");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFSettlementOperatorSettlement_NPFSettlementOperator_NPFSettlementOperatorsId",
                table: "NPFSettlementOperatorSettlement");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFStateAdminState_NPFStateAdmins_NPFStateAdminsId",
                table: "NPFStateAdminState");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFStateOperatorState_NPFStateOperator_NPFStateOperatorsId",
                table: "NPFStateOperatorState");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFTownAdminTown_NPFTownAdmin_NPFTownAdminsId",
                table: "NPFTownAdminTown");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFTownOperatorTown_NPFTownOperator_NPFTownOperatorsId",
                table: "NPFTownOperatorTown");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteSettlementAdminSettlement_OfficialVigilanteSettlementAdmins_OfficialVigilanteSettlementAdminsId",
                table: "OfficialVigilanteSettlementAdminSettlement");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteSettlementOperatorSettlement_OfficialVigilanteSettlementOperators_OfficialVigilanteSettlementOperatorsId",
                table: "OfficialVigilanteSettlementOperatorSettlement");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteStateAdminState_OfficialVigilanteStateAdmins_OfficialVigilanteStateAdminsId",
                table: "OfficialVigilanteStateAdminState");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteStateOperatorState_OfficialVigilanteStateOperators_OfficialVigilanteStateOperatorsId",
                table: "OfficialVigilanteStateOperatorState");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_VGNGAStaff_VGNGAAdminAuthorizerId",
                table: "SecurityTips");

            migrationBuilder.DropForeignKey(
                name: "FK_SettlementCurfew_VGNGAStaff_AdminAuthorizerID",
                table: "SettlementCurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_SettlementVGNGAStaff_VGNGAStaff_VGNGASettlementAdminsId",
                table: "SettlementVGNGAStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_SettlementVGNGAStaff1_VGNGAStaff_VGNGASettlementOperatorsId",
                table: "SettlementVGNGAStaff1");

            migrationBuilder.DropForeignKey(
                name: "FK_StateCurfew_VGNGAStaff_AdminAuthorizerID",
                table: "StateCurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_StateVGNGAStaff_VGNGAStaff_VGNGAStateAdminsId",
                table: "StateVGNGAStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_StateVGNGAStaff1_VGNGAStaff_VGNGAStateOperatorsId",
                table: "StateVGNGAStaff1");

            migrationBuilder.DropForeignKey(
                name: "FK_TownCurfew_VGNGAStaff_AdminAuthorizerID",
                table: "TownCurfew");

            migrationBuilder.DropForeignKey(
                name: "FK_TownVGNGAStaff_VGNGAStaff_VGNGATownAdminsId",
                table: "TownVGNGAStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_TownVGNGAStaff1_VGNGAStaff_VGNGATownOperatorsId",
                table: "TownVGNGAStaff1");

            migrationBuilder.DropTable(
                name: "NPFLGAAdmin");

            migrationBuilder.DropTable(
                name: "NPFLGAOperator");

            migrationBuilder.DropTable(
                name: "NPFSettlementAdmin");

            migrationBuilder.DropTable(
                name: "NPFSettlementOperator");

            migrationBuilder.DropTable(
                name: "NPFStateAdmins");

            migrationBuilder.DropTable(
                name: "NPFStateOperator");

            migrationBuilder.DropTable(
                name: "NPFTownAdmin");

            migrationBuilder.DropTable(
                name: "NPFTownOperator");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteLGAAdmins");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteLGAOperators");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteSettlementOperators");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropTable(
                name: "OfficialVigilanteStateOperators");

            migrationBuilder.DropTable(
                name: "VGNGAStaff");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NPFLGAAdmin_IsSuperAdmin",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NPFSettlementAdmin_IsSuperAdmin",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NPFStateAdmin_IsSuperAdmin",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NPFTownAdmin_IsSuperAdmin",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OfficialVigilanteSettlementAdmin_IsSuperAdmin",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OfficialVigilanteStateAdmin_IsSuperAdmin",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OfficialVigilanteTownAdmin_IsSuperAdmin",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Users",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalaryCurrency",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StaffId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

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
                name: "FK_LGACurfew_Users_AdminAuthorizerID",
                table: "LGACurfew",
                column: "AdminAuthorizerID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LGANPFLGAAdmin_Users_NPFLGAAdminsId",
                table: "LGANPFLGAAdmin",
                column: "NPFLGAAdminsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LGANPFLGAOperator_Users_NPFLGAOperatorsId",
                table: "LGANPFLGAOperator",
                column: "NPFLGAOperatorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LGAOfficialVigilanteLGAAdmin_Users_OfficialVigilanteLGAAdminsId",
                table: "LGAOfficialVigilanteLGAAdmin",
                column: "OfficialVigilanteLGAAdminsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LGAOfficialVigilanteLGAOperator_Users_OfficialVigilanteLGAOperatorsId",
                table: "LGAOfficialVigilanteLGAOperator",
                column: "OfficialVigilanteLGAOperatorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LGAVGNGAStaff_Users_VGNGALGAOperatorsId",
                table: "LGAVGNGAStaff",
                column: "VGNGALGAOperatorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LGAVGNGAStaff1_Users_VGNGALGAAdminsId",
                table: "LGAVGNGAStaff1",
                column: "VGNGALGAAdminsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissingItem_Users_AdminAuthorizerID",
                table: "MissingItem",
                column: "AdminAuthorizerID",
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
                name: "FK_NPFSettlementAdminSettlement_Users_NPFSettlementAdminsId",
                table: "NPFSettlementAdminSettlement",
                column: "NPFSettlementAdminsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFSettlementOperatorSettlement_Users_NPFSettlementOperatorsId",
                table: "NPFSettlementOperatorSettlement",
                column: "NPFSettlementOperatorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFStateAdminState_Users_NPFStateAdminsId",
                table: "NPFStateAdminState",
                column: "NPFStateAdminsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFStateOperatorState_Users_NPFStateOperatorsId",
                table: "NPFStateOperatorState",
                column: "NPFStateOperatorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFTownAdminTown_Users_NPFTownAdminsId",
                table: "NPFTownAdminTown",
                column: "NPFTownAdminsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFTownOperatorTown_Users_NPFTownOperatorsId",
                table: "NPFTownOperatorTown",
                column: "NPFTownOperatorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteSettlementAdminSettlement_Users_OfficialVigilanteSettlementAdminsId",
                table: "OfficialVigilanteSettlementAdminSettlement",
                column: "OfficialVigilanteSettlementAdminsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteSettlementOperatorSettlement_Users_OfficialVigilanteSettlementOperatorsId",
                table: "OfficialVigilanteSettlementOperatorSettlement",
                column: "OfficialVigilanteSettlementOperatorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteStateAdminState_Users_OfficialVigilanteStateAdminsId",
                table: "OfficialVigilanteStateAdminState",
                column: "OfficialVigilanteStateAdminsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteStateOperatorState_Users_OfficialVigilanteStateOperatorsId",
                table: "OfficialVigilanteStateOperatorState",
                column: "OfficialVigilanteStateOperatorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_Users_VGNGAAdminAuthorizerId",
                table: "SecurityTips",
                column: "VGNGAAdminAuthorizerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SettlementCurfew_Users_AdminAuthorizerID",
                table: "SettlementCurfew",
                column: "AdminAuthorizerID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SettlementVGNGAStaff_Users_VGNGASettlementAdminsId",
                table: "SettlementVGNGAStaff",
                column: "VGNGASettlementAdminsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SettlementVGNGAStaff1_Users_VGNGASettlementOperatorsId",
                table: "SettlementVGNGAStaff1",
                column: "VGNGASettlementOperatorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StateCurfew_Users_AdminAuthorizerID",
                table: "StateCurfew",
                column: "AdminAuthorizerID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StateVGNGAStaff_Users_VGNGAStateAdminsId",
                table: "StateVGNGAStaff",
                column: "VGNGAStateAdminsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StateVGNGAStaff1_Users_VGNGAStateOperatorsId",
                table: "StateVGNGAStaff1",
                column: "VGNGAStateOperatorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TownCurfew_Users_AdminAuthorizerID",
                table: "TownCurfew",
                column: "AdminAuthorizerID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TownVGNGAStaff_Users_VGNGATownAdminsId",
                table: "TownVGNGAStaff",
                column: "VGNGATownAdminsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TownVGNGAStaff1_Users_VGNGATownOperatorsId",
                table: "TownVGNGAStaff1",
                column: "VGNGATownOperatorsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
