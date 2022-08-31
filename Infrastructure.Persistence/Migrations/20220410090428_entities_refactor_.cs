using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class entities_refactor_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OfficialVigilanteLGAAdmin_IsSuperAdmin",
                table: "Users",
                newName: "NPFLGAAdmin_IsSuperAdmin");

            migrationBuilder.AlterColumn<string>(
                name: "photoUrl",
                table: "MissingPerson",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NPFLGAAdmin_IsSuperAdmin",
                table: "Users",
                newName: "OfficialVigilanteLGAAdmin_IsSuperAdmin");

            migrationBuilder.AlterColumn<int>(
                name: "photoUrl",
                table: "MissingPerson",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
