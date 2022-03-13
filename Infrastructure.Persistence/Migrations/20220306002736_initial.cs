using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Infrastructure.Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlertLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    alertLevel = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BroadcasterTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Broadcaster = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BroadcasterTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BroadcastLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    broadcastLevel = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BroadcastLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "demographicEntitiesCoordinatesJSONs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DemographicType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JsonString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_demographicEntitiesCoordinatesJSONs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecurityTipCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityTipCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isVGNGAStaff = table.Column<bool>(type: "bit", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TempLocationID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastCurrentLocationID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueReferalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstTimePromoUsed = table.Column<bool>(type: "bit", nullable: false),
                    ActiveStatus = table.Column<bool>(type: "bit", nullable: false),
                    CurrentPromoUsed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NPFOperatorStateId = table.Column<int>(type: "int", nullable: true),
                    NPFAdminStateId = table.Column<int>(type: "int", nullable: true),
                    VigilanteAdminStateId = table.Column<int>(type: "int", nullable: true),
                    VigilanteOperatorStateId = table.Column<int>(type: "int", nullable: true),
                    NPFAdminLGAId = table.Column<int>(type: "int", nullable: true),
                    NPFOperatorLGAId = table.Column<int>(type: "int", nullable: true),
                    VigilanteAdminLGAId = table.Column<int>(type: "int", nullable: true),
                    VigilanteOperatorLGAId = table.Column<int>(type: "int", nullable: true),
                    NPFAdminTownId = table.Column<int>(type: "int", nullable: true),
                    NPFOperatorTownId = table.Column<int>(type: "int", nullable: true),
                    VigilanteAdminTownId = table.Column<int>(type: "int", nullable: true),
                    VigilanteOperatorTownId = table.Column<int>(type: "int", nullable: true),
                    NPFOperatorSettlementId = table.Column<int>(type: "int", nullable: true),
                    NPFAdminSettlementId = table.Column<int>(type: "int", nullable: true),
                    VigilanteAdminSettlementId = table.Column<int>(type: "int", nullable: true),
                    VigilanteOperatorSettlementId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => new { x.ApplicationUserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityTips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isAdminAuthorized = table.Column<bool>(type: "bit", nullable: false),
                    AdminAuthorizerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    isVGNGAAuthorized = table.Column<bool>(type: "bit", nullable: false),
                    VGNGAAuthorizerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SettlementId = table.Column<int>(type: "int", nullable: true),
                    TownId = table.Column<int>(type: "int", nullable: true),
                    LGAId = table.Column<int>(type: "int", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    BroadcasterTypeId = table.Column<int>(type: "int", nullable: false),
                    BroadcasterUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SecurityTipCategoryId = table.Column<int>(type: "int", nullable: false),
                    AlertLevelId = table.Column<int>(type: "int", nullable: false),
                    BroadcastLevelId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityTips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityTips_Users_AdminAuthorizerID",
                        column: x => x.AdminAuthorizerID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityTips_Users_BroadcasterUserId",
                        column: x => x.BroadcasterUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityTips_Users_VGNGAAuthorizerID",
                        column: x => x.VGNGAAuthorizerID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LGAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    LGASuperAdminId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NPFLGAAuthorityAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NPFPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isCapital = table.Column<bool>(type: "bit", nullable: false),
                    SecurityTipId = table.Column<int>(type: "int", nullable: true),
                    Boundary = table.Column<Geometry>(type: "geography", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LGAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LGAs_SecurityTips_SecurityTipId",
                        column: x => x.SecurityTipId,
                        principalTable: "SecurityTips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TownId = table.Column<int>(type: "int", nullable: false),
                    NPFAdminId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NPFAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NPFPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityTipId = table.Column<int>(type: "int", nullable: true),
                    TownSuperAdminId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settlements_SecurityTips_SecurityTipId",
                        column: x => x.SecurityTipId,
                        principalTable: "SecurityTips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NPFStatePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NPFStateAuthorityAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateSuperAdminId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityTipId = table.Column<int>(type: "int", nullable: true),
                    shapeLength = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    shapeArea = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Boundary = table.Column<Geometry>(type: "geography", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_SecurityTips_SecurityTipId",
                        column: x => x.SecurityTipId,
                        principalTable: "SecurityTips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LGAId = table.Column<int>(type: "int", nullable: false),
                    NPFAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NPFPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TownSuperAdminId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityTipId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Towns_SecurityTips_SecurityTipId",
                        column: x => x.SecurityTipId,
                        principalTable: "SecurityTips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LGAs_SecurityTipId",
                table: "LGAs",
                column: "SecurityTipId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_AdminAuthorizerID",
                table: "SecurityTips",
                column: "AdminAuthorizerID");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_BroadcasterUserId",
                table: "SecurityTips",
                column: "BroadcasterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_VGNGAAuthorizerID",
                table: "SecurityTips",
                column: "VGNGAAuthorizerID");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_SecurityTipId",
                table: "Settlements",
                column: "SecurityTipId");

            migrationBuilder.CreateIndex(
                name: "IX_States_SecurityTipId",
                table: "States",
                column: "SecurityTipId");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_SecurityTipId",
                table: "Towns",
                column: "SecurityTipId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NPFAdminLGAId",
                table: "Users",
                column: "NPFAdminLGAId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NPFAdminSettlementId",
                table: "Users",
                column: "NPFAdminSettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NPFAdminStateId",
                table: "Users",
                column: "NPFAdminStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NPFAdminTownId",
                table: "Users",
                column: "NPFAdminTownId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NPFOperatorLGAId",
                table: "Users",
                column: "NPFOperatorLGAId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NPFOperatorSettlementId",
                table: "Users",
                column: "NPFOperatorSettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NPFOperatorStateId",
                table: "Users",
                column: "NPFOperatorStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NPFOperatorTownId",
                table: "Users",
                column: "NPFOperatorTownId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VigilanteAdminLGAId",
                table: "Users",
                column: "VigilanteAdminLGAId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VigilanteAdminSettlementId",
                table: "Users",
                column: "VigilanteAdminSettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VigilanteAdminStateId",
                table: "Users",
                column: "VigilanteAdminStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VigilanteAdminTownId",
                table: "Users",
                column: "VigilanteAdminTownId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VigilanteOperatorLGAId",
                table: "Users",
                column: "VigilanteOperatorLGAId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VigilanteOperatorSettlementId",
                table: "Users",
                column: "VigilanteOperatorSettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VigilanteOperatorStateId",
                table: "Users",
                column: "VigilanteOperatorStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VigilanteOperatorTownId",
                table: "Users",
                column: "VigilanteOperatorTownId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LGAs_NPFAdminLGAId",
                table: "Users",
                column: "NPFAdminLGAId",
                principalTable: "LGAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LGAs_NPFOperatorLGAId",
                table: "Users",
                column: "NPFOperatorLGAId",
                principalTable: "LGAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LGAs_VigilanteAdminLGAId",
                table: "Users",
                column: "VigilanteAdminLGAId",
                principalTable: "LGAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LGAs_VigilanteOperatorLGAId",
                table: "Users",
                column: "VigilanteOperatorLGAId",
                principalTable: "LGAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Settlements_NPFAdminSettlementId",
                table: "Users",
                column: "NPFAdminSettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Settlements_NPFOperatorSettlementId",
                table: "Users",
                column: "NPFOperatorSettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Settlements_VigilanteAdminSettlementId",
                table: "Users",
                column: "VigilanteAdminSettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Settlements_VigilanteOperatorSettlementId",
                table: "Users",
                column: "VigilanteOperatorSettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_States_NPFAdminStateId",
                table: "Users",
                column: "NPFAdminStateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_States_NPFOperatorStateId",
                table: "Users",
                column: "NPFOperatorStateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_States_VigilanteAdminStateId",
                table: "Users",
                column: "VigilanteAdminStateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_States_VigilanteOperatorStateId",
                table: "Users",
                column: "VigilanteOperatorStateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Towns_NPFAdminTownId",
                table: "Users",
                column: "NPFAdminTownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Towns_NPFOperatorTownId",
                table: "Users",
                column: "NPFOperatorTownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Towns_VigilanteAdminTownId",
                table: "Users",
                column: "VigilanteAdminTownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Towns_VigilanteOperatorTownId",
                table: "Users",
                column: "VigilanteOperatorTownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LGAs_SecurityTips_SecurityTipId",
                table: "LGAs");

            migrationBuilder.DropForeignKey(
                name: "FK_Settlements_SecurityTips_SecurityTipId",
                table: "Settlements");

            migrationBuilder.DropForeignKey(
                name: "FK_States_SecurityTips_SecurityTipId",
                table: "States");

            migrationBuilder.DropForeignKey(
                name: "FK_Towns_SecurityTips_SecurityTipId",
                table: "Towns");

            migrationBuilder.DropTable(
                name: "AlertLevels");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "BroadcasterTypes");

            migrationBuilder.DropTable(
                name: "BroadcastLevels");

            migrationBuilder.DropTable(
                name: "CustomClaims");

            migrationBuilder.DropTable(
                name: "demographicEntitiesCoordinatesJSONs");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "SecurityTipCategories");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "SecurityTips");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LGAs");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Towns");
        }
    }
}
