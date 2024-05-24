using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatelocationmodelsformattedaddressfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsFormattedAddress",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsFormattedAddress",
                table: "States",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsFormattedAddress",
                table: "LGAs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsFormattedAddress",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleMapsFormattedAddress",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "GoogleMapsFormattedAddress",
                table: "States");

            migrationBuilder.DropColumn(
                name: "GoogleMapsFormattedAddress",
                table: "LGAs");

            migrationBuilder.DropColumn(
                name: "GoogleMapsFormattedAddress",
                table: "Countries");
        }
    }
}
