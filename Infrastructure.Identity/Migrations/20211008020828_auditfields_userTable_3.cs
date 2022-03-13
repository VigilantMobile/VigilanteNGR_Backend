using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Identity.Migrations
{
    public partial class auditfields_userTable_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
               name: "CreatedBy",
                table: "User",
                schema: "Identity",
               nullable: true);

            migrationBuilder.AddColumn<DateTime>(
           name: "Created",
           table: "User",
            schema: "Identity",
            nullable: true);

            migrationBuilder.AddColumn<string>(
              name: "LastModifiedBy",
              table: "User",
                schema: "Identity",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
              name: "LastModified",
              table: "User",
               schema: "Identity",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
