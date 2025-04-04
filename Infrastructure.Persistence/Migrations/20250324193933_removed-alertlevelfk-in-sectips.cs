using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removedalertlevelfkinsectips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_AlertLevels_AlertLevelId",
                table: "SecurityTips");

            migrationBuilder.AlterColumn<Guid>(
                name: "AlertLevelId",
                table: "SecurityTips",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "AlertLevel",
                table: "SecurityTips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_AlertLevels_AlertLevelId",
                table: "SecurityTips",
                column: "AlertLevelId",
                principalTable: "AlertLevels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecurityTips_AlertLevels_AlertLevelId",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "AlertLevel",
                table: "SecurityTips");

            migrationBuilder.AlterColumn<Guid>(
                name: "AlertLevelId",
                table: "SecurityTips",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityTips_AlertLevels_AlertLevelId",
                table: "SecurityTips",
                column: "AlertLevelId",
                principalTable: "AlertLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
