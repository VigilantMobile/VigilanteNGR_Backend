using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class tipcategorytypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isAdminAuthorized",
                table: "SecurityTips",
                newName: "IsAdminAuthorized");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "SecurityTipCategories",
                newName: "Name");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryTypeId",
                table: "SecurityTipCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SecurityTipCategoryTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityTipCategoryTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTipCategories_CategoryTypeId",
                table: "SecurityTipCategories",
                column: "CategoryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTipCategories_SecurityTipCategoryTypes_CategoryTypeId",
                table: "SecurityTipCategories",
                column: "CategoryTypeId",
                principalTable: "SecurityTipCategoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTipCategories_SecurityTipCategoryTypes_CategoryTypeId",
                table: "SecurityTipCategories");

            migrationBuilder.DropTable(
                name: "SecurityTipCategoryTypes");

            migrationBuilder.DropIndex(
                name: "IX_SecurityTipCategories_CategoryTypeId",
                table: "SecurityTipCategories");

            migrationBuilder.DropColumn(
                name: "CategoryTypeId",
                table: "SecurityTipCategories");

            migrationBuilder.RenameColumn(
                name: "IsAdminAuthorized",
                table: "SecurityTips",
                newName: "isAdminAuthorized");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SecurityTipCategories",
                newName: "CategoryName");
        }
    }
}
