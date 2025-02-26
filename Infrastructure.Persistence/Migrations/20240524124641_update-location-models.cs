using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatelocationmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Countries",
                newName: "GoogleMapsShortName");

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsGeometryInfo",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsLocationType",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsLongName",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsShortName",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsGeometryInfo",
                table: "States",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsLocationType",
                table: "States",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsLongName",
                table: "States",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsShortName",
                table: "States",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsGeometryInfo",
                table: "LGAs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsLocationType",
                table: "LGAs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsLongName",
                table: "LGAs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsShortName",
                table: "LGAs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsGeometryInfo",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsLocationType",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsLongName",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleMapsGeometryInfo",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "GoogleMapsLocationType",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "GoogleMapsLongName",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "GoogleMapsShortName",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "GoogleMapsGeometryInfo",
                table: "States");

            migrationBuilder.DropColumn(
                name: "GoogleMapsLocationType",
                table: "States");

            migrationBuilder.DropColumn(
                name: "GoogleMapsLongName",
                table: "States");

            migrationBuilder.DropColumn(
                name: "GoogleMapsShortName",
                table: "States");

            migrationBuilder.DropColumn(
                name: "GoogleMapsGeometryInfo",
                table: "LGAs");

            migrationBuilder.DropColumn(
                name: "GoogleMapsLocationType",
                table: "LGAs");

            migrationBuilder.DropColumn(
                name: "GoogleMapsLongName",
                table: "LGAs");

            migrationBuilder.DropColumn(
                name: "GoogleMapsShortName",
                table: "LGAs");

            migrationBuilder.DropColumn(
                name: "GoogleMapsGeometryInfo",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "GoogleMapsLocationType",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "GoogleMapsLongName",
                table: "Countries");

            migrationBuilder.RenameColumn(
                name: "GoogleMapsShortName",
                table: "Countries",
                newName: "ShortName");
        }
    }
}
