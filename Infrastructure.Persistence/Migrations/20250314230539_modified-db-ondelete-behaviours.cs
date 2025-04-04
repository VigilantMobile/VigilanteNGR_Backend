using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class modifieddbondeletebehaviours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isProfileVisible",
                table: "UserCircle",
                newName: "InviterProfileVisible");

            migrationBuilder.AddColumn<bool>(
                name: "InviteeProfileVisible",
                table: "UserCircle",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InviteeProfileVisible",
                table: "UserCircle");

            migrationBuilder.RenameColumn(
                name: "InviterProfileVisible",
                table: "UserCircle",
                newName: "isProfileVisible");
        }
    }
}
