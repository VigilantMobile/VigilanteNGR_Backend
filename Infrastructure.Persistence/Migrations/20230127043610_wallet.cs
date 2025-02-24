using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class wallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_SecurityTips_Source_SourceId",
            //    table: "SecurityTips");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Source_SourceCategories_SourceCategoryId",
            //    table: "Source");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Source",
            //    table: "Source");

            //migrationBuilder.DropColumn(
            //    name: "IsAppAdmin",
            //    table: "Users");

            //migrationBuilder.DropColumn(
            //    name: "IsAppOperator",
            //    table: "Users");

            //migrationBuilder.DropColumn(
            //    name: "IsAppSuperAdmin",
            //    table: "Users");

            //migrationBuilder.DropColumn(
            //    name: "Salary",
            //    table: "Users");

            //migrationBuilder.DropColumn(
            //    name: "SalaryCurrency",
            //    table: "Users");

            //migrationBuilder.DropColumn(
            //    name: "StaffId",
            //    table: "Users");

            //migrationBuilder.RenameTable(
            //    name: "Source",
            //    newName: "Sources");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Source_SourceCategoryId",
            //    table: "Sources",
            //    newName: "IX_Sources_SourceCategoryId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Sources",
            //    table: "Sources",
            //    column: "Id");

            //migrationBuilder.CreateTable(
            //    name: "Wallets",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        WalletBalance = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
            //        ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Created = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Wallets", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Wallets_Users_ApplicationUserId",
            //            column: x => x.ApplicationUserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Wallets_ApplicationUserId",
            //    table: "Wallets",
            //    column: "ApplicationUserId",
            //    unique: true,
            //    filter: "[ApplicationUserId] IS NOT NULL");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_SecurityTips_Sources_SourceId",
            //    table: "SecurityTips",
            //    column: "SourceId",
            //    principalTable: "Sources",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Sources_SourceCategories_SourceCategoryId",
            //    table: "Sources",
            //    column: "SourceCategoryId",
            //    principalTable: "SourceCategories",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_SecurityTips_Sources_SourceId",
            //    table: "SecurityTips");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Sources_SourceCategories_SourceCategoryId",
            //    table: "Sources");

            //migrationBuilder.DropTable(
            //    name: "Wallets");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Sources",
            //    table: "Sources");

            //migrationBuilder.RenameTable(
            //    name: "Sources",
            //    newName: "Source");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Sources_SourceCategoryId",
            //    table: "Source",
            //    newName: "IX_Source_SourceCategoryId");

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsAppAdmin",
            //    table: "Users",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsAppOperator",
            //    table: "Users",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsAppSuperAdmin",
            //    table: "Users",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<decimal>(
            //    name: "Salary",
            //    table: "Users",
            //    type: "decimal(18,6)",
            //    nullable: false,
            //    defaultValue: 0m);

            //migrationBuilder.AddColumn<string>(
            //    name: "SalaryCurrency",
            //    table: "Users",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "StaffId",
            //    table: "Users",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Source",
            //    table: "Source",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_SecurityTips_Source_SourceId",
            //    table: "SecurityTips",
            //    column: "SourceId",
            //    principalTable: "Source",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Source_SourceCategories_SourceCategoryId",
            //    table: "Source",
            //    column: "SourceCategoryId",
            //    principalTable: "SourceCategories",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
