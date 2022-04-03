using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Infrastructure.Persistence.Migrations
{
    public partial class __initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlertLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NPFStateAuthorityPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NPFStateAuthorityAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "LGAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    NPFLGAAuthorityAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NPFPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isCapital = table.Column<bool>(type: "bit", nullable: false),
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
                        name: "FK_LGAs_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LGAId = table.Column<int>(type: "int", nullable: false),
                    NPFTownAuthorityAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NPFPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Boundary = table.Column<Geometry>(type: "geography", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Towns_LGAs_LGAId",
                        column: x => x.LGAId,
                        principalTable: "LGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TownId = table.Column<int>(type: "int", nullable: false),
                    NPFSettlementAuthorityAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorityPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Boundary = table.Column<Geometry>(type: "geography", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settlements_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommuteRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureCoordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureTownId = table.Column<int>(type: "int", maxLength: 150, nullable: false),
                    DepartureTownAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationTownId = table.Column<int>(type: "int", nullable: false),
                    DestinationCoordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationTownAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureSettlementId = table.Column<int>(type: "int", nullable: false),
                    DepartureSettlementAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationSettlementId = table.Column<int>(type: "int", nullable: false),
                    DestinationSettlementAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisiteeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisiteePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurposeOfVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalTripInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PanicIntervalInMinutes = table.Column<int>(type: "int", nullable: false),
                    CommuteStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PanicStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PanicInitiated = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommuteRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommuteRecords_Settlements_DepartureSettlementId",
                        column: x => x.DepartureSettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommuteRecords_Settlements_DestinationSettlementId",
                        column: x => x.DestinationSettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommuteRecords_Towns_DepartureTownId",
                        column: x => x.DepartureTownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommuteRecords_Towns_DestinationTownId",
                        column: x => x.DestinationTownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WantedPerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    complexion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    age = table.Column<int>(type: "int", nullable: false),
                    photoUrl = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateLastSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TownId = table.Column<int>(type: "int", nullable: false),
                    SettlementId = table.Column<int>(type: "int", nullable: false),
                    isAuthorized = table.Column<bool>(type: "bit", nullable: false),
                    NPFAuthorizerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NPFAuthorityType = table.Column<int>(type: "int", nullable: false),
                    BroadcastLevelId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WantedPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WantedPerson_BroadcastLevels_BroadcastLevelId",
                        column: x => x.BroadcastLevelId,
                        principalTable: "BroadcastLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WantedPerson_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WantedPerson_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SecurityTips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BroadcasterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isAdminAuthorized = table.Column<bool>(type: "bit", nullable: false),
                    AdminAuthorizerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VGNGAAdminAuthorizerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    BroadcastLevelId = table.Column<int>(type: "int", nullable: false),
                    BroadcasterTypeId = table.Column<int>(type: "int", nullable: false),
                    SecurityTipCategoryId = table.Column<int>(type: "int", nullable: false),
                    AlertLevelId = table.Column<int>(type: "int", nullable: false),
                    LGAId = table.Column<int>(type: "int", nullable: true),
                    SettlementId = table.Column<int>(type: "int", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    TownId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityTips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityTips_AlertLevels_AlertLevelId",
                        column: x => x.AlertLevelId,
                        principalTable: "AlertLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecurityTips_BroadcasterTypes_BroadcasterTypeId",
                        column: x => x.BroadcasterTypeId,
                        principalTable: "BroadcasterTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecurityTips_BroadcastLevels_BroadcastLevelId",
                        column: x => x.BroadcastLevelId,
                        principalTable: "BroadcastLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecurityTips_LGAs_LGAId",
                        column: x => x.LGAId,
                        principalTable: "LGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityTips_SecurityTipCategories_SecurityTipCategoryId",
                        column: x => x.SecurityTipCategoryId,
                        principalTable: "SecurityTipCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecurityTips_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityTips_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityTips_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MissingItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateLastSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TownId = table.Column<int>(type: "int", nullable: false),
                    SettlementId = table.Column<int>(type: "int", nullable: false),
                    isAdminAuthorized = table.Column<bool>(type: "bit", nullable: false),
                    LoserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AdminAuthorizerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BroadcastLevelId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissingItem_BroadcastLevels_BroadcastLevelId",
                        column: x => x.BroadcastLevelId,
                        principalTable: "BroadcastLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissingItem_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MissingItem_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MissingPerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    complexion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    age = table.Column<int>(type: "int", nullable: false),
                    photoUrl = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateLastSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TownId = table.Column<int>(type: "int", nullable: true),
                    SettlementId = table.Column<int>(type: "int", nullable: false),
                    isAdminAuthorized = table.Column<bool>(type: "bit", nullable: false),
                    LoserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AdminAuthorizerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BroadcastLevelId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissingPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissingPerson_BroadcastLevels_BroadcastLevelId",
                        column: x => x.BroadcastLevelId,
                        principalTable: "BroadcastLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissingPerson_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MissingPerson_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PanicRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PanicInitiator = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PanicStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommuteId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PanicRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PanicRecords_CommuteRecords_CommuteId",
                        column: x => x.CommuteId,
                        principalTable: "CommuteRecords",
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
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    LGAId = table.Column<int>(type: "int", nullable: false),
                    TownId = table.Column<int>(type: "int", nullable: false),
                    SettlementId = table.Column<int>(type: "int", nullable: false),
                    LastLocationCoordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueReferalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstTimePromoUsed = table.Column<bool>(type: "bit", nullable: false),
                    ActiveStatus = table.Column<bool>(type: "bit", nullable: false),
                    CurrentPromoUsed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSuperAdmin = table.Column<bool>(type: "bit", nullable: true),
                    NPFSettlementAdmin_IsSuperAdmin = table.Column<bool>(type: "bit", nullable: true),
                    NPFStateAdmin_IsSuperAdmin = table.Column<bool>(type: "bit", nullable: true),
                    NPFTownAdmin_IsSuperAdmin = table.Column<bool>(type: "bit", nullable: true),
                    OfficialVigilanteLGAAdmin_IsSuperAdmin = table.Column<bool>(type: "bit", nullable: true),
                    OfficialVigilanteSettlementAdmin_IsSuperAdmin = table.Column<bool>(type: "bit", nullable: true),
                    OfficialVigilanteStateAdmin_IsSuperAdmin = table.Column<bool>(type: "bit", nullable: true),
                    OfficialVigilanteTownAdmin_IsSuperAdmin = table.Column<bool>(type: "bit", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    StaffId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    SalaryCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Users_LGAs_LGAId",
                        column: x => x.LGAId,
                        principalTable: "LGAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Settlements_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HodId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SecretaryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Users_HodId",
                        column: x => x.HodId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Departments_Users_SecretaryId",
                        column: x => x.SecretaryId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LGACurfew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LGAId = table.Column<int>(type: "int", nullable: false),
                    AdminAuthorizerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DailyStartHour = table.Column<int>(type: "int", nullable: false),
                    DailyEndHour = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_LGACurfew_Users_AdminAuthorizerID",
                        column: x => x.AdminAuthorizerID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_LGANPFLGAAdmin_Users_NPFLGAAdminsId",
                        column: x => x.NPFLGAAdminsId,
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
                        name: "FK_LGANPFLGAOperator_Users_NPFLGAOperatorsId",
                        column: x => x.NPFLGAOperatorsId,
                        principalTable: "Users",
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
                        name: "FK_LGAOfficialVigilanteLGAAdmin_Users_OfficialVigilanteLGAAdminsId",
                        column: x => x.OfficialVigilanteLGAAdminsId,
                        principalTable: "Users",
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
                        name: "FK_LGAOfficialVigilanteLGAOperator_Users_OfficialVigilanteLGAOperatorsId",
                        column: x => x.OfficialVigilanteLGAOperatorsId,
                        principalTable: "Users",
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
                        name: "FK_LGAVGNGAStaff_Users_VGNGALGAOperatorsId",
                        column: x => x.VGNGALGAOperatorsId,
                        principalTable: "Users",
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
                        name: "FK_LGAVGNGAStaff1_Users_VGNGALGAAdminsId",
                        column: x => x.VGNGALGAAdminsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_NPFSettlementAdminSettlement_Settlements_SettlementsId",
                        column: x => x.SettlementsId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPFSettlementAdminSettlement_Users_NPFSettlementAdminsId",
                        column: x => x.NPFSettlementAdminsId,
                        principalTable: "Users",
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
                        name: "FK_NPFSettlementOperatorSettlement_Settlements_SettlementsId",
                        column: x => x.SettlementsId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPFSettlementOperatorSettlement_Users_NPFSettlementOperatorsId",
                        column: x => x.NPFSettlementOperatorsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_NPFStateAdminState_States_StatesId",
                        column: x => x.StatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPFStateAdminState_Users_NPFStateAdminsId",
                        column: x => x.NPFStateAdminsId,
                        principalTable: "Users",
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
                        name: "FK_NPFStateOperatorState_States_StatesId",
                        column: x => x.StatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPFStateOperatorState_Users_NPFStateOperatorsId",
                        column: x => x.NPFStateOperatorsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_NPFTownAdminTown_Towns_TownsId",
                        column: x => x.TownsId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPFTownAdminTown_Users_NPFTownAdminsId",
                        column: x => x.NPFTownAdminsId,
                        principalTable: "Users",
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
                        name: "FK_NPFTownOperatorTown_Towns_TownsId",
                        column: x => x.TownsId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NPFTownOperatorTown_Users_NPFTownOperatorsId",
                        column: x => x.NPFTownOperatorsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_OfficialVigilanteSettlementAdminSettlement_Settlements_SettlementsId",
                        column: x => x.SettlementsId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteSettlementAdminSettlement_Users_OfficialVigilanteSettlementAdminsId",
                        column: x => x.OfficialVigilanteSettlementAdminsId,
                        principalTable: "Users",
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
                        name: "FK_OfficialVigilanteSettlementOperatorSettlement_Settlements_SettlementsId",
                        column: x => x.SettlementsId,
                        principalTable: "Settlements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteSettlementOperatorSettlement_Users_OfficialVigilanteSettlementOperatorsId",
                        column: x => x.OfficialVigilanteSettlementOperatorsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_OfficialVigilanteStateAdminState_States_StatesId",
                        column: x => x.StatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteStateAdminState_Users_OfficialVigilanteStateAdminsId",
                        column: x => x.OfficialVigilanteStateAdminsId,
                        principalTable: "Users",
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
                        name: "FK_OfficialVigilanteStateOperatorState_States_StatesId",
                        column: x => x.StatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfficialVigilanteStateOperatorState_Users_OfficialVigilanteStateOperatorsId",
                        column: x => x.OfficialVigilanteStateOperatorsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "SettlementCurfew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettlementId = table.Column<int>(type: "int", nullable: false),
                    AdminAuthorizerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DailyStartHour = table.Column<int>(type: "int", nullable: false),
                    DailyEndHour = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_SettlementCurfew_Users_AdminAuthorizerID",
                        column: x => x.AdminAuthorizerID,
                        principalTable: "Users",
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
                        name: "FK_SettlementVGNGAStaff_Users_VGNGASettlementAdminsId",
                        column: x => x.VGNGASettlementAdminsId,
                        principalTable: "Users",
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
                        name: "FK_SettlementVGNGAStaff1_Users_VGNGASettlementOperatorsId",
                        column: x => x.VGNGASettlementOperatorsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateCurfew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    AdminAuthorizerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DailyStartHour = table.Column<int>(type: "int", nullable: false),
                    DailyEndHour = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateCurfew", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateCurfew_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StateCurfew_Users_AdminAuthorizerID",
                        column: x => x.AdminAuthorizerID,
                        principalTable: "Users",
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
                        name: "FK_StateVGNGAStaff_Users_VGNGAStateAdminsId",
                        column: x => x.VGNGAStateAdminsId,
                        principalTable: "Users",
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
                        name: "FK_StateVGNGAStaff1_Users_VGNGAStateOperatorsId",
                        column: x => x.VGNGAStateOperatorsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TownCurfew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TownId = table.Column<int>(type: "int", nullable: false),
                    AdminAuthorizerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DailyStartHour = table.Column<int>(type: "int", nullable: false),
                    DailyEndHour = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_TownCurfew_Users_AdminAuthorizerID",
                        column: x => x.AdminAuthorizerID,
                        principalTable: "Users",
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
                        name: "FK_TownVGNGAStaff_Users_VGNGATownAdminsId",
                        column: x => x.VGNGATownAdminsId,
                        principalTable: "Users",
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
                        name: "FK_TownVGNGAStaff1_Users_VGNGATownOperatorsId",
                        column: x => x.VGNGATownOperatorsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_CommuteRecords_DepartureSettlementId",
                table: "CommuteRecords",
                column: "DepartureSettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_CommuteRecords_DepartureTownId",
                table: "CommuteRecords",
                column: "DepartureTownId");

            migrationBuilder.CreateIndex(
                name: "IX_CommuteRecords_DestinationSettlementId",
                table: "CommuteRecords",
                column: "DestinationSettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_CommuteRecords_DestinationTownId",
                table: "CommuteRecords",
                column: "DestinationTownId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HodId",
                table: "Departments",
                column: "HodId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_SecretaryId",
                table: "Departments",
                column: "SecretaryId");

            migrationBuilder.CreateIndex(
                name: "IX_LGACurfew_AdminAuthorizerID",
                table: "LGACurfew",
                column: "AdminAuthorizerID");

            migrationBuilder.CreateIndex(
                name: "IX_LGACurfew_LGAId",
                table: "LGACurfew",
                column: "LGAId");

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
                name: "IX_LGAs_StateId",
                table: "LGAs",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_LGAVGNGAStaff_VGNGAOperatorLGAsId",
                table: "LGAVGNGAStaff",
                column: "VGNGAOperatorLGAsId");

            migrationBuilder.CreateIndex(
                name: "IX_LGAVGNGAStaff1_VGNGALGAAdminsId",
                table: "LGAVGNGAStaff1",
                column: "VGNGALGAAdminsId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingItem_AdminAuthorizerID",
                table: "MissingItem",
                column: "AdminAuthorizerID");

            migrationBuilder.CreateIndex(
                name: "IX_MissingItem_BroadcastLevelId",
                table: "MissingItem",
                column: "BroadcastLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingItem_LoserId",
                table: "MissingItem",
                column: "LoserId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingItem_SettlementId",
                table: "MissingItem",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingItem_TownId",
                table: "MissingItem",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingPerson_AdminAuthorizerID",
                table: "MissingPerson",
                column: "AdminAuthorizerID");

            migrationBuilder.CreateIndex(
                name: "IX_MissingPerson_BroadcastLevelId",
                table: "MissingPerson",
                column: "BroadcastLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingPerson_LoserId",
                table: "MissingPerson",
                column: "LoserId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingPerson_SettlementId",
                table: "MissingPerson",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingPerson_TownId",
                table: "MissingPerson",
                column: "TownId");

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

            migrationBuilder.CreateIndex(
                name: "IX_PanicRecords_CommuteId",
                table: "PanicRecords",
                column: "CommuteId");

            migrationBuilder.CreateIndex(
                name: "IX_PanicRecords_PanicInitiator",
                table: "PanicRecords",
                column: "PanicInitiator");

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
                name: "IX_SecurityTips_AlertLevelId",
                table: "SecurityTips",
                column: "AlertLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_BroadcasterId",
                table: "SecurityTips",
                column: "BroadcasterId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_BroadcasterTypeId",
                table: "SecurityTips",
                column: "BroadcasterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_BroadcastLevelId",
                table: "SecurityTips",
                column: "BroadcastLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_LGAId",
                table: "SecurityTips",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_SecurityTipCategoryId",
                table: "SecurityTips",
                column: "SecurityTipCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_SettlementId",
                table: "SecurityTips",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_StateId",
                table: "SecurityTips",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_TownId",
                table: "SecurityTips",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTips_VGNGAAdminAuthorizerId",
                table: "SecurityTips",
                column: "VGNGAAdminAuthorizerId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementCurfew_AdminAuthorizerID",
                table: "SettlementCurfew",
                column: "AdminAuthorizerID");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementCurfew_SettlementId",
                table: "SettlementCurfew",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_TownId",
                table: "Settlements",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementVGNGAStaff_VGNGASettlementAdminsId",
                table: "SettlementVGNGAStaff",
                column: "VGNGASettlementAdminsId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementVGNGAStaff1_VGNGASettlementOperatorsId",
                table: "SettlementVGNGAStaff1",
                column: "VGNGASettlementOperatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_StateCurfew_AdminAuthorizerID",
                table: "StateCurfew",
                column: "AdminAuthorizerID");

            migrationBuilder.CreateIndex(
                name: "IX_StateCurfew_StateId",
                table: "StateCurfew",
                column: "StateId");

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
                name: "IX_Towns_LGAId",
                table: "Towns",
                column: "LGAId");

            migrationBuilder.CreateIndex(
                name: "IX_TownVGNGAStaff_VGNGATownAdminsId",
                table: "TownVGNGAStaff",
                column: "VGNGATownAdminsId");

            migrationBuilder.CreateIndex(
                name: "IX_TownVGNGAStaff1_VGNGATownOperatorsId",
                table: "TownVGNGAStaff1",
                column: "VGNGATownOperatorsId");

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
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_TownId",
                table: "Users",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WantedPerson_BroadcastLevelId",
                table: "WantedPerson",
                column: "BroadcastLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_WantedPerson_SettlementId",
                table: "WantedPerson",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_WantedPerson_TownId",
                table: "WantedPerson",
                column: "TownId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_Users_BroadcasterId",
                table: "SecurityTips",
                column: "BroadcasterId",
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
                name: "FK_MissingItem_Users_AdminAuthorizerID",
                table: "MissingItem",
                column: "AdminAuthorizerID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MissingItem_Users_LoserId",
                table: "MissingItem",
                column: "LoserId",
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
                name: "FK_MissingPerson_Users_LoserId",
                table: "MissingPerson",
                column: "LoserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PanicRecords_Users_PanicInitiator",
                table: "PanicRecords",
                column: "PanicInitiator",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Settlements_SettlementId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Towns_TownId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_HodId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Users_SecretaryId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "CustomClaims");

            migrationBuilder.DropTable(
                name: "demographicEntitiesCoordinatesJSONs");

            migrationBuilder.DropTable(
                name: "LGACurfew");

            migrationBuilder.DropTable(
                name: "LGANPFLGAAdmin");

            migrationBuilder.DropTable(
                name: "LGANPFLGAOperator");

            migrationBuilder.DropTable(
                name: "LGAOfficialVigilanteLGAAdmin");

            migrationBuilder.DropTable(
                name: "LGAOfficialVigilanteLGAOperator");

            migrationBuilder.DropTable(
                name: "LGAVGNGAStaff");

            migrationBuilder.DropTable(
                name: "LGAVGNGAStaff1");

            migrationBuilder.DropTable(
                name: "MissingItem");

            migrationBuilder.DropTable(
                name: "MissingPerson");

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
                name: "PanicRecords");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "SecurityTips");

            migrationBuilder.DropTable(
                name: "SettlementCurfew");

            migrationBuilder.DropTable(
                name: "SettlementVGNGAStaff");

            migrationBuilder.DropTable(
                name: "SettlementVGNGAStaff1");

            migrationBuilder.DropTable(
                name: "StateCurfew");

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
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "WantedPerson");

            migrationBuilder.DropTable(
                name: "CommuteRecords");

            migrationBuilder.DropTable(
                name: "AlertLevels");

            migrationBuilder.DropTable(
                name: "BroadcasterTypes");

            migrationBuilder.DropTable(
                name: "SecurityTipCategories");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "BroadcastLevels");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropTable(
                name: "Towns");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "LGAs");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
