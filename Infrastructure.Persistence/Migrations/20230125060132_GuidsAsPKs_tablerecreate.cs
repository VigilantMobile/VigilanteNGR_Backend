using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class GuidsAsPKs_tablerecreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "AlertLevels",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        alertLevel = table.Column<int>(type: "int", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AlertLevels", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AuditLogs",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AuditLogs", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "BroadcasterTypes",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Broadcaster = table.Column<int>(type: "int", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BroadcasterTypes", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "BroadcastLevels",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        broadcastLevel = table.Column<int>(type: "int", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BroadcastLevels", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CustomClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        type = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        value = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CustomClaims", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "demographicEntitiesCoordinatesJSONs",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        DemographicType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        JsonString = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_demographicEntitiesCoordinatesJSONs", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Products",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Rate = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Products", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Roles",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Roles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SecurityTipCategories",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SecurityTipCategories", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SecurityTipStatuses",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        TipStatus = table.Column<int>(type: "int", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SecurityTipStatuses", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SourceCategories",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SourceCategories", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "States",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        NPFStateAuthorityPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NPFStateAuthorityAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        shapeLength = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
            //        shapeArea = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
            //        Boundary = table.Column<Geometry>(type: "geography", nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_States", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Subscriptions",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        SubscriptionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SubscriptionDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MonthlyFee = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Subscriptions", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RoleClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RoleClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_RoleClaims_Roles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "Roles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Source",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        SourceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        IsVerified = table.Column<bool>(type: "bit", nullable: false),
            //        LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SourceCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Source", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Source_SourceCategories_SourceCategoryId",
            //            column: x => x.SourceCategoryId,
            //            principalTable: "SourceCategories",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LGAs",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        NPFLGAAuthorityAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NPFPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        isCapital = table.Column<bool>(type: "bit", nullable: false),
            //        Boundary = table.Column<Geometry>(type: "geography", nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LGAs", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LGAs_States_StateId",
            //            column: x => x.StateId,
            //            principalTable: "States",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Towns",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        LGAId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        NPFTownAuthorityAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NPFPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Boundary = table.Column<Geometry>(type: "geography", nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Towns", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Towns_LGAs_LGAId",
            //            column: x => x.LGAId,
            //            principalTable: "LGAs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Settlements",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TownId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        NPFSettlementAuthorityAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        AuthorityPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Boundary = table.Column<Geometry>(type: "geography", nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Settlements", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Settlements_Towns_TownId",
            //            column: x => x.TownId,
            //            principalTable: "Towns",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CommuteRecords",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        DepartureCoordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DepartureTownId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 150, nullable: false),
            //        DepartureTownAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DestinationTownId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        DestinationCoordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DestinationTownAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DepartureSettlementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        DepartureSettlementAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DestinationSettlementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        DestinationSettlementAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        VisiteeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        VisiteePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PurposeOfVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        AdditionalTripInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PanicIntervalInMinutes = table.Column<int>(type: "int", nullable: false),
            //        CommuteStatus = table.Column<int>(type: "int", nullable: false),
            //        PanicStatus = table.Column<int>(type: "int", nullable: false),
            //        PanicInitiated = table.Column<bool>(type: "bit", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CommuteRecords", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_CommuteRecords_Settlements_DepartureSettlementId",
            //            column: x => x.DepartureSettlementId,
            //            principalTable: "Settlements",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_CommuteRecords_Settlements_DestinationSettlementId",
            //            column: x => x.DestinationSettlementId,
            //            principalTable: "Settlements",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_CommuteRecords_Towns_DepartureTownId",
            //            column: x => x.DepartureTownId,
            //            principalTable: "Towns",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_CommuteRecords_Towns_DestinationTownId",
            //            column: x => x.DestinationTownId,
            //            principalTable: "Towns",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WantedPerson",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //        MiddleName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
            //        LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //        Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        complexion = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        age = table.Column<int>(type: "int", nullable: false),
            //        photoUrl = table.Column<int>(type: "int", nullable: false),
            //        description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //        DateLastSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        TownId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        SettlementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        isAuthorized = table.Column<bool>(type: "bit", nullable: false),
            //        NPFAuthorizerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NPFAuthorityType = table.Column<int>(type: "int", nullable: false),
            //        BroadcastLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WantedPerson", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WantedPerson_BroadcastLevels_BroadcastLevelId",
            //            column: x => x.BroadcastLevelId,
            //            principalTable: "BroadcastLevels",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_WantedPerson_Settlements_SettlementId",
            //            column: x => x.SettlementId,
            //            principalTable: "Settlements",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_WantedPerson_Towns_TownId",
            //            column: x => x.TownId,
            //            principalTable: "Towns",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ApplicationUserLGA",
            //    columns: table => new
            //    {
            //        InternalStaffLGAsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        VGNGALGAStaffId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ApplicationUserLGA", x => new { x.InternalStaffLGAsId, x.VGNGALGAStaffId });
            //        table.ForeignKey(
            //            name: "FK_ApplicationUserLGA_LGAs_InternalStaffLGAsId",
            //            column: x => x.InternalStaffLGAsId,
            //            principalTable: "LGAs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ApplicationUserSettlement",
            //    columns: table => new
            //    {
            //        InternalStaffSettlementsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        VGNGASettlementStaffId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ApplicationUserSettlement", x => new { x.InternalStaffSettlementsId, x.VGNGASettlementStaffId });
            //        table.ForeignKey(
            //            name: "FK_ApplicationUserSettlement_Settlements_InternalStaffSettlementsId",
            //            column: x => x.InternalStaffSettlementsId,
            //            principalTable: "Settlements",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ApplicationUserState",
            //    columns: table => new
            //    {
            //        InternalStaffStatesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        VGNGAStateStaffId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ApplicationUserState", x => new { x.InternalStaffStatesId, x.VGNGAStateStaffId });
            //        table.ForeignKey(
            //            name: "FK_ApplicationUserState_States_InternalStaffStatesId",
            //            column: x => x.InternalStaffStatesId,
            //            principalTable: "States",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ApplicationUserTown",
            //    columns: table => new
            //    {
            //        InternalStaffTownsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        VGNGATownStaffId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ApplicationUserTown", x => new { x.InternalStaffTownsId, x.VGNGATownStaffId });
            //        table.ForeignKey(
            //            name: "FK_ApplicationUserTown_Towns_InternalStaffTownsId",
            //            column: x => x.InternalStaffTownsId,
            //            principalTable: "Towns",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CommentFlags",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        VoterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CommentVote = table.Column<int>(type: "int", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CommentFlags", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Comments",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        UserComment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        SecurityTipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CommenterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Comments", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Departments",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        HodId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        SecretaryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Departments", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TownId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        UniqueReferalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        FirstTimePromoUsed = table.Column<bool>(type: "bit", nullable: false),
            //        isActive = table.Column<bool>(type: "bit", nullable: false),
            //        CurrentPromoUsed = table.Column<bool>(type: "bit", nullable: false),
            //        DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        StaffId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Salary = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
            //        SalaryCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IsAppSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
            //        IsAppAdmin = table.Column<bool>(type: "bit", nullable: false),
            //        IsAppOperator = table.Column<bool>(type: "bit", nullable: false),
            //        ExternalStaffType = table.Column<int>(type: "int", nullable: false),
            //        IsExternalSuperAdmin = table.Column<bool>(type: "bit", nullable: false),
            //        IsExternalAdmin = table.Column<bool>(type: "bit", nullable: false),
            //        IsExternalOperator = table.Column<bool>(type: "bit", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BroadcastLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Users", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Users_BroadcastLevels_BroadcastLevelId",
            //            column: x => x.BroadcastLevelId,
            //            principalTable: "BroadcastLevels",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Users_Departments_DepartmentId",
            //            column: x => x.DepartmentId,
            //            principalTable: "Departments",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Users_Subscriptions_SubscriptionId",
            //            column: x => x.SubscriptionId,
            //            principalTable: "Subscriptions",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Users_Towns_TownId",
            //            column: x => x.TownId,
            //            principalTable: "Towns",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MissingItem",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        ItemName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //        ItemType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //        IdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DateLastSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        TownId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        SettlementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        isAdminAuthorized = table.Column<bool>(type: "bit", nullable: false),
            //        LoserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        AdminAuthorizerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        VGNGAStaffId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        BroadcastLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MissingItem", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MissingItem_BroadcastLevels_BroadcastLevelId",
            //            column: x => x.BroadcastLevelId,
            //            principalTable: "BroadcastLevels",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_MissingItem_Settlements_SettlementId",
            //            column: x => x.SettlementId,
            //            principalTable: "Settlements",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MissingItem_Towns_TownId",
            //            column: x => x.TownId,
            //            principalTable: "Towns",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MissingItem_Users_LoserId",
            //            column: x => x.LoserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MissingItem_Users_VGNGAStaffId",
            //            column: x => x.VGNGAStaffId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MissingPerson",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //        MiddleName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
            //        LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //        Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        complexion = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        age = table.Column<int>(type: "int", nullable: false),
            //        photoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //        DateLastSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        TownId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        SettlementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        isAdminAuthorized = table.Column<bool>(type: "bit", nullable: false),
            //        LoserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        AdminAuthorizerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        BroadcastLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MissingPerson", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MissingPerson_BroadcastLevels_BroadcastLevelId",
            //            column: x => x.BroadcastLevelId,
            //            principalTable: "BroadcastLevels",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_MissingPerson_Settlements_SettlementId",
            //            column: x => x.SettlementId,
            //            principalTable: "Settlements",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MissingPerson_Towns_TownId",
            //            column: x => x.TownId,
            //            principalTable: "Towns",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_MissingPerson_Users_AdminAuthorizerID",
            //            column: x => x.AdminAuthorizerID,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_MissingPerson_Users_LoserId",
            //            column: x => x.LoserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PanicRecords",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        PanicInitiator = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        PanicStatus = table.Column<int>(type: "int", nullable: false),
            //        PanicType = table.Column<int>(type: "int", nullable: false),
            //        CommuteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PanicRecords", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_PanicRecords_CommuteRecords_CommuteId",
            //            column: x => x.CommuteId,
            //            principalTable: "CommuteRecords",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_PanicRecords_Users_PanicInitiator",
            //            column: x => x.PanicInitiator,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SecurityTips",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Subject = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Body = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        IsBroadcasted = table.Column<bool>(type: "bit", nullable: false),
            //        Casualties = table.Column<int>(type: "int", nullable: false),
            //        SecurityTipStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        TipStatusString = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        BroadcasterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        isAdminAuthorized = table.Column<bool>(type: "bit", nullable: false),
            //        AdminAuthorizerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        ExternalInitiatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        ExternalAuthorizerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        LocationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BroadcastLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        EscalationRequested = table.Column<bool>(type: "bit", nullable: false),
            //        BroadcasterTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        BroadcasterTypeString = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        SecurityTipCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        AlertLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        LGAId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        SettlementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        TownId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SecurityTips", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_SecurityTips_AlertLevels_AlertLevelId",
            //            column: x => x.AlertLevelId,
            //            principalTable: "AlertLevels",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_SecurityTips_BroadcasterTypes_BroadcasterTypeId",
            //            column: x => x.BroadcasterTypeId,
            //            principalTable: "BroadcasterTypes",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_SecurityTips_BroadcastLevels_BroadcastLevelId",
            //            column: x => x.BroadcastLevelId,
            //            principalTable: "BroadcastLevels",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_SecurityTips_LGAs_LGAId",
            //            column: x => x.LGAId,
            //            principalTable: "LGAs",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_SecurityTips_SecurityTipCategories_SecurityTipCategoryId",
            //            column: x => x.SecurityTipCategoryId,
            //            principalTable: "SecurityTipCategories",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_SecurityTips_SecurityTipStatuses_SecurityTipStatusId",
            //            column: x => x.SecurityTipStatusId,
            //            principalTable: "SecurityTipStatuses",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_SecurityTips_Settlements_SettlementId",
            //            column: x => x.SettlementId,
            //            principalTable: "Settlements",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_SecurityTips_Source_SourceId",
            //            column: x => x.SourceId,
            //            principalTable: "Source",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_SecurityTips_States_StateId",
            //            column: x => x.StateId,
            //            principalTable: "States",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_SecurityTips_Towns_TownId",
            //            column: x => x.TownId,
            //            principalTable: "Towns",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_SecurityTips_Users_AdminAuthorizerID",
            //            column: x => x.AdminAuthorizerID,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_SecurityTips_Users_BroadcasterId",
            //            column: x => x.BroadcasterId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_SecurityTips_Users_ExternalAuthorizerId",
            //            column: x => x.ExternalAuthorizerId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_SecurityTips_Users_ExternalInitiatorId",
            //            column: x => x.ExternalInitiatorId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "StateCurfew",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        DailyStartHour = table.Column<int>(type: "int", nullable: false),
            //        DailyEndHour = table.Column<int>(type: "int", nullable: false),
            //        AdminAuthorizerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        OperatorIniatorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        IsAuthorized = table.Column<bool>(type: "bit", nullable: false),
            //        StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        LGAId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        TownId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        SettlementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_StateCurfew", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_StateCurfew_LGAs_LGAId",
            //            column: x => x.LGAId,
            //            principalTable: "LGAs",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_StateCurfew_Settlements_SettlementId",
            //            column: x => x.SettlementId,
            //            principalTable: "Settlements",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_StateCurfew_States_StateId",
            //            column: x => x.StateId,
            //            principalTable: "States",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_StateCurfew_Towns_TownId",
            //            column: x => x.TownId,
            //            principalTable: "Towns",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_StateCurfew_Users_AdminAuthorizerId",
            //            column: x => x.AdminAuthorizerId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_StateCurfew_Users_OperatorIniatorId",
            //            column: x => x.OperatorIniatorId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TrustedPeople",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TrustedPeople", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_TrustedPeople_Users_OwnerId",
            //            column: x => x.OwnerId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserClaims_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserLogins",
            //    columns: table => new
            //    {
            //        LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
            //        table.ForeignKey(
            //            name: "FK_UserLogins_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserRoles",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
            //        table.ForeignKey(
            //            name: "FK_UserRoles_Roles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "Roles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_UserRoles_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserTokens",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            //        table.ForeignKey(
            //            name: "FK_UserTokens_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EscalatedTips",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        SecurityTipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        EscalationLocationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EscalationBroadcastLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        EscalationAuthorizerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        isEscalationApproved = table.Column<bool>(type: "bit", nullable: false),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EscalatedTips", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_EscalatedTips_BroadcastLevels_EscalationBroadcastLevelId",
            //            column: x => x.EscalationBroadcastLevelId,
            //            principalTable: "BroadcastLevels",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_EscalatedTips_SecurityTips_SecurityTipId",
            //            column: x => x.SecurityTipId,
            //            principalTable: "SecurityTips",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_EscalatedTips_Users_EscalationAuthorizerID",
            //            column: x => x.EscalationAuthorizerID,
            //            principalTable: "Users",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ApplicationUserLGA_VGNGALGAStaffId",
            //    table: "ApplicationUserLGA",
            //    column: "VGNGALGAStaffId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ApplicationUserSettlement_VGNGASettlementStaffId",
            //    table: "ApplicationUserSettlement",
            //    column: "VGNGASettlementStaffId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ApplicationUserState_VGNGAStateStaffId",
            //    table: "ApplicationUserState",
            //    column: "VGNGAStateStaffId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ApplicationUserTown_VGNGATownStaffId",
            //    table: "ApplicationUserTown",
            //    column: "VGNGATownStaffId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CommentFlags_CommentId",
            //    table: "CommentFlags",
            //    column: "CommentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CommentFlags_VoterId",
            //    table: "CommentFlags",
            //    column: "VoterId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Comments_CommenterId",
            //    table: "Comments",
            //    column: "CommenterId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Comments_SecurityTipId",
            //    table: "Comments",
            //    column: "SecurityTipId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CommuteRecords_DepartureSettlementId",
            //    table: "CommuteRecords",
            //    column: "DepartureSettlementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CommuteRecords_DepartureTownId",
            //    table: "CommuteRecords",
            //    column: "DepartureTownId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CommuteRecords_DestinationSettlementId",
            //    table: "CommuteRecords",
            //    column: "DestinationSettlementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CommuteRecords_DestinationTownId",
            //    table: "CommuteRecords",
            //    column: "DestinationTownId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Departments_HodId",
            //    table: "Departments",
            //    column: "HodId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Departments_SecretaryId",
            //    table: "Departments",
            //    column: "SecretaryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EscalatedTips_EscalationAuthorizerID",
            //    table: "EscalatedTips",
            //    column: "EscalationAuthorizerID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EscalatedTips_EscalationBroadcastLevelId",
            //    table: "EscalatedTips",
            //    column: "EscalationBroadcastLevelId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EscalatedTips_SecurityTipId",
            //    table: "EscalatedTips",
            //    column: "SecurityTipId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LGAs_StateId",
            //    table: "LGAs",
            //    column: "StateId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MissingItem_BroadcastLevelId",
            //    table: "MissingItem",
            //    column: "BroadcastLevelId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MissingItem_LoserId",
            //    table: "MissingItem",
            //    column: "LoserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MissingItem_SettlementId",
            //    table: "MissingItem",
            //    column: "SettlementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MissingItem_TownId",
            //    table: "MissingItem",
            //    column: "TownId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MissingItem_VGNGAStaffId",
            //    table: "MissingItem",
            //    column: "VGNGAStaffId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MissingPerson_AdminAuthorizerID",
            //    table: "MissingPerson",
            //    column: "AdminAuthorizerID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MissingPerson_BroadcastLevelId",
            //    table: "MissingPerson",
            //    column: "BroadcastLevelId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MissingPerson_LoserId",
            //    table: "MissingPerson",
            //    column: "LoserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MissingPerson_SettlementId",
            //    table: "MissingPerson",
            //    column: "SettlementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MissingPerson_TownId",
            //    table: "MissingPerson",
            //    column: "TownId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PanicRecords_CommuteId",
            //    table: "PanicRecords",
            //    column: "CommuteId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PanicRecords_PanicInitiator",
            //    table: "PanicRecords",
            //    column: "PanicInitiator");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RoleClaims_RoleId",
            //    table: "RoleClaims",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "RoleNameIndex",
            //    table: "Roles",
            //    column: "NormalizedName",
            //    unique: true,
            //    filter: "[NormalizedName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SecurityTips_AdminAuthorizerID",
            //    table: "SecurityTips",
            //    column: "AdminAuthorizerID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SecurityTips_AlertLevelId",
            //    table: "SecurityTips",
            //    column: "AlertLevelId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SecurityTips_BroadcasterId",
            //    table: "SecurityTips",
            //    column: "BroadcasterId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SecurityTips_BroadcasterTypeId",
            //    table: "SecurityTips",
            //    column: "BroadcasterTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SecurityTips_BroadcastLevelId",
            //    table: "SecurityTips",
            //    column: "BroadcastLevelId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SecurityTips_ExternalAuthorizerId",
            //    table: "SecurityTips",
            //    column: "ExternalAuthorizerId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SecurityTips_ExternalInitiatorId",
            //    table: "SecurityTips",
            //    column: "ExternalInitiatorId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SecurityTips_LGAId",
            //    table: "SecurityTips",
            //    column: "LGAId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SecurityTips_SecurityTipCategoryId",
            //    table: "SecurityTips",
            //    column: "SecurityTipCategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SecurityTips_SecurityTipStatusId",
            //    table: "SecurityTips",
            //    column: "SecurityTipStatusId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SecurityTips_SettlementId",
            //    table: "SecurityTips",
            //    column: "SettlementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SecurityTips_SourceId",
            //    table: "SecurityTips",
            //    column: "SourceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SecurityTips_StateId",
            //    table: "SecurityTips",
            //    column: "StateId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SecurityTips_TownId",
            //    table: "SecurityTips",
            //    column: "TownId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Settlements_TownId",
            //    table: "Settlements",
            //    column: "TownId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Source_SourceCategoryId",
            //    table: "Source",
            //    column: "SourceCategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_StateCurfew_AdminAuthorizerId",
            //    table: "StateCurfew",
            //    column: "AdminAuthorizerId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_StateCurfew_LGAId",
            //    table: "StateCurfew",
            //    column: "LGAId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_StateCurfew_OperatorIniatorId",
            //    table: "StateCurfew",
            //    column: "OperatorIniatorId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_StateCurfew_SettlementId",
            //    table: "StateCurfew",
            //    column: "SettlementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_StateCurfew_StateId",
            //    table: "StateCurfew",
            //    column: "StateId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_StateCurfew_TownId",
            //    table: "StateCurfew",
            //    column: "TownId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Towns_LGAId",
            //    table: "Towns",
            //    column: "LGAId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TrustedPeople_OwnerId",
            //    table: "TrustedPeople",
            //    column: "OwnerId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserClaims_UserId",
            //    table: "UserClaims",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserLogins_UserId",
            //    table: "UserLogins",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserRoles_RoleId",
            //    table: "UserRoles",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "EmailIndex",
            //    table: "Users",
            //    column: "NormalizedEmail");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_BroadcastLevelId",
            //    table: "Users",
            //    column: "BroadcastLevelId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_DepartmentId",
            //    table: "Users",
            //    column: "DepartmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_SubscriptionId",
            //    table: "Users",
            //    column: "SubscriptionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_TownId",
            //    table: "Users",
            //    column: "TownId");

            //migrationBuilder.CreateIndex(
            //    name: "UserNameIndex",
            //    table: "Users",
            //    column: "NormalizedUserName",
            //    unique: true,
            //    filter: "[NormalizedUserName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WantedPerson_BroadcastLevelId",
            //    table: "WantedPerson",
            //    column: "BroadcastLevelId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WantedPerson_SettlementId",
            //    table: "WantedPerson",
            //    column: "SettlementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WantedPerson_TownId",
            //    table: "WantedPerson",
            //    column: "TownId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ApplicationUserLGA_Users_VGNGALGAStaffId",
            //    table: "ApplicationUserLGA",
            //    column: "VGNGALGAStaffId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ApplicationUserSettlement_Users_VGNGASettlementStaffId",
            //    table: "ApplicationUserSettlement",
            //    column: "VGNGASettlementStaffId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ApplicationUserState_Users_VGNGAStateStaffId",
            //    table: "ApplicationUserState",
            //    column: "VGNGAStateStaffId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ApplicationUserTown_Users_VGNGATownStaffId",
            //    table: "ApplicationUserTown",
            //    column: "VGNGATownStaffId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CommentFlags_Comments_CommentId",
            //    table: "CommentFlags",
            //    column: "CommentId",
            //    principalTable: "Comments",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CommentFlags_Users_VoterId",
            //    table: "CommentFlags",
            //    column: "VoterId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Comments_SecurityTips_SecurityTipId",
            //    table: "Comments",
            //    column: "SecurityTipId",
            //    principalTable: "SecurityTips",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Comments_Users_CommenterId",
            //    table: "Comments",
            //    column: "CommenterId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Departments_Users_HodId",
            //    table: "Departments",
            //    column: "HodId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Departments_Users_SecretaryId",
            //    table: "Departments",
            //    column: "SecretaryId",
            //    principalTable: "Users",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Towns_LGAs_LGAId",
            //    table: "Towns");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Departments_Users_HodId",
            //    table: "Departments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Departments_Users_SecretaryId",
            //    table: "Departments");

            //migrationBuilder.DropTable(
            //    name: "ApplicationUserLGA");

            //migrationBuilder.DropTable(
            //    name: "ApplicationUserSettlement");

            //migrationBuilder.DropTable(
            //    name: "ApplicationUserState");

            //migrationBuilder.DropTable(
            //    name: "ApplicationUserTown");

            //migrationBuilder.DropTable(
            //    name: "AuditLogs");

            //migrationBuilder.DropTable(
            //    name: "CommentFlags");

            //migrationBuilder.DropTable(
            //    name: "CustomClaims");

            //migrationBuilder.DropTable(
            //    name: "demographicEntitiesCoordinatesJSONs");

            //migrationBuilder.DropTable(
            //    name: "EscalatedTips");

            //migrationBuilder.DropTable(
            //    name: "MissingItem");

            //migrationBuilder.DropTable(
            //    name: "MissingPerson");

            //migrationBuilder.DropTable(
            //    name: "PanicRecords");

            //migrationBuilder.DropTable(
            //    name: "Products");

            //migrationBuilder.DropTable(
            //    name: "RoleClaims");

            //migrationBuilder.DropTable(
            //    name: "StateCurfew");

            //migrationBuilder.DropTable(
            //    name: "TrustedPeople");

            //migrationBuilder.DropTable(
            //    name: "UserClaims");

            //migrationBuilder.DropTable(
            //    name: "UserLogins");

            //migrationBuilder.DropTable(
            //    name: "UserRoles");

            //migrationBuilder.DropTable(
            //    name: "UserTokens");

            //migrationBuilder.DropTable(
            //    name: "WantedPerson");

            //migrationBuilder.DropTable(
            //    name: "Comments");

            //migrationBuilder.DropTable(
            //    name: "CommuteRecords");

            //migrationBuilder.DropTable(
            //    name: "Roles");

            //migrationBuilder.DropTable(
            //    name: "SecurityTips");

            //migrationBuilder.DropTable(
            //    name: "AlertLevels");

            //migrationBuilder.DropTable(
            //    name: "BroadcasterTypes");

            //migrationBuilder.DropTable(
            //    name: "SecurityTipCategories");

            //migrationBuilder.DropTable(
            //    name: "SecurityTipStatuses");

            //migrationBuilder.DropTable(
            //    name: "Settlements");

            //migrationBuilder.DropTable(
            //    name: "Source");

            //migrationBuilder.DropTable(
            //    name: "SourceCategories");

            //migrationBuilder.DropTable(
            //    name: "LGAs");

            //migrationBuilder.DropTable(
            //    name: "States");

            //migrationBuilder.DropTable(
            //    name: "Users");

            //migrationBuilder.DropTable(
            //    name: "BroadcastLevels");

            //migrationBuilder.DropTable(
            //    name: "Departments");

            //migrationBuilder.DropTable(
            //    name: "Subscriptions");

            //migrationBuilder.DropTable(
            //    name: "Towns");
        }
    }
}
