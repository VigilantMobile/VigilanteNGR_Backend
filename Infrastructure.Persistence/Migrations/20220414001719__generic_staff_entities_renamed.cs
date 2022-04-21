using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class _generic_staff_entities_renamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NPFLGAAdmin_LGAs_LGAId",
                table: "NPFLGAAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFStateAdmins_States_StateId",
                table: "NPFStateAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFTownAdmin_Towns_TownId",
                table: "NPFTownAdmin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NPFTownAdmin",
                table: "NPFTownAdmin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NPFStateAdmins",
                table: "NPFStateAdmins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NPFLGAAdmin",
                table: "NPFLGAAdmin");

            migrationBuilder.RenameTable(
                name: "NPFTownAdmin",
                newName: "NPFTownStaff");

            migrationBuilder.RenameTable(
                name: "NPFStateAdmins",
                newName: "NPFStateStaff");

            migrationBuilder.RenameTable(
                name: "NPFLGAAdmin",
                newName: "NPFLGAStaff");

            migrationBuilder.RenameIndex(
                name: "IX_NPFTownAdmin_TownId",
                table: "NPFTownStaff",
                newName: "IX_NPFTownStaff_TownId");

            migrationBuilder.RenameIndex(
                name: "IX_NPFStateAdmins_StateId",
                table: "NPFStateStaff",
                newName: "IX_NPFStateStaff_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_NPFLGAAdmin_LGAId",
                table: "NPFLGAStaff",
                newName: "IX_NPFLGAStaff_LGAId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NPFTownStaff",
                table: "NPFTownStaff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NPFStateStaff",
                table: "NPFStateStaff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NPFLGAStaff",
                table: "NPFLGAStaff",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NPFLGAStaff_LGAs_LGAId",
                table: "NPFLGAStaff",
                column: "LGAId",
                principalTable: "LGAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFStateStaff_States_StateId",
                table: "NPFStateStaff",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFTownStaff_Towns_TownId",
                table: "NPFTownStaff",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NPFLGAStaff_LGAs_LGAId",
                table: "NPFLGAStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFStateStaff_States_StateId",
                table: "NPFStateStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_NPFTownStaff_Towns_TownId",
                table: "NPFTownStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NPFTownStaff",
                table: "NPFTownStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NPFStateStaff",
                table: "NPFStateStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NPFLGAStaff",
                table: "NPFLGAStaff");

            migrationBuilder.RenameTable(
                name: "NPFTownStaff",
                newName: "NPFTownAdmin");

            migrationBuilder.RenameTable(
                name: "NPFStateStaff",
                newName: "NPFStateAdmins");

            migrationBuilder.RenameTable(
                name: "NPFLGAStaff",
                newName: "NPFLGAAdmin");

            migrationBuilder.RenameIndex(
                name: "IX_NPFTownStaff_TownId",
                table: "NPFTownAdmin",
                newName: "IX_NPFTownAdmin_TownId");

            migrationBuilder.RenameIndex(
                name: "IX_NPFStateStaff_StateId",
                table: "NPFStateAdmins",
                newName: "IX_NPFStateAdmins_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_NPFLGAStaff_LGAId",
                table: "NPFLGAAdmin",
                newName: "IX_NPFLGAAdmin_LGAId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NPFTownAdmin",
                table: "NPFTownAdmin",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NPFStateAdmins",
                table: "NPFStateAdmins",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NPFLGAAdmin",
                table: "NPFLGAAdmin",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NPFLGAAdmin_LGAs_LGAId",
                table: "NPFLGAAdmin",
                column: "LGAId",
                principalTable: "LGAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFStateAdmins_States_StateId",
                table: "NPFStateAdmins",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NPFTownAdmin_Towns_TownId",
                table: "NPFTownAdmin",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
