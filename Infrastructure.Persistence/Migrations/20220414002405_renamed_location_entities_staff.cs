using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class renamed_location_entities_staff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteLGAAdmins_LGAs_LGAId",
                table: "OfficialVigilanteLGAAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteSettlementAdmins_Settlements_SettlementId",
                table: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteStateAdmins_States_StateId",
                table: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteTownAdmins_Towns_TownId",
                table: "OfficialVigilanteTownAdmins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfficialVigilanteTownAdmins",
                table: "OfficialVigilanteTownAdmins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfficialVigilanteStateAdmins",
                table: "OfficialVigilanteStateAdmins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfficialVigilanteSettlementAdmins",
                table: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfficialVigilanteLGAAdmins",
                table: "OfficialVigilanteLGAAdmins");

            migrationBuilder.RenameTable(
                name: "OfficialVigilanteTownAdmins",
                newName: "OfficialVigilanteTownStaff");

            migrationBuilder.RenameTable(
                name: "OfficialVigilanteStateAdmins",
                newName: "OfficialVigilanteStateStaff");

            migrationBuilder.RenameTable(
                name: "OfficialVigilanteSettlementAdmins",
                newName: "OfficialVigilanteSettlementStaff");

            migrationBuilder.RenameTable(
                name: "OfficialVigilanteLGAAdmins",
                newName: "OfficialVigilanteLGAStaff");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialVigilanteTownAdmins_TownId",
                table: "OfficialVigilanteTownStaff",
                newName: "IX_OfficialVigilanteTownStaff_TownId");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialVigilanteStateAdmins_StateId",
                table: "OfficialVigilanteStateStaff",
                newName: "IX_OfficialVigilanteStateStaff_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialVigilanteSettlementAdmins_SettlementId",
                table: "OfficialVigilanteSettlementStaff",
                newName: "IX_OfficialVigilanteSettlementStaff_SettlementId");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialVigilanteLGAAdmins_LGAId",
                table: "OfficialVigilanteLGAStaff",
                newName: "IX_OfficialVigilanteLGAStaff_LGAId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfficialVigilanteTownStaff",
                table: "OfficialVigilanteTownStaff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfficialVigilanteStateStaff",
                table: "OfficialVigilanteStateStaff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfficialVigilanteSettlementStaff",
                table: "OfficialVigilanteSettlementStaff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfficialVigilanteLGAStaff",
                table: "OfficialVigilanteLGAStaff",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteLGAStaff_LGAs_LGAId",
                table: "OfficialVigilanteLGAStaff",
                column: "LGAId",
                principalTable: "LGAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteSettlementStaff_Settlements_SettlementId",
                table: "OfficialVigilanteSettlementStaff",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteStateStaff_States_StateId",
                table: "OfficialVigilanteStateStaff",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteTownStaff_Towns_TownId",
                table: "OfficialVigilanteTownStaff",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteLGAStaff_LGAs_LGAId",
                table: "OfficialVigilanteLGAStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteSettlementStaff_Settlements_SettlementId",
                table: "OfficialVigilanteSettlementStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteStateStaff_States_StateId",
                table: "OfficialVigilanteStateStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialVigilanteTownStaff_Towns_TownId",
                table: "OfficialVigilanteTownStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfficialVigilanteTownStaff",
                table: "OfficialVigilanteTownStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfficialVigilanteStateStaff",
                table: "OfficialVigilanteStateStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfficialVigilanteSettlementStaff",
                table: "OfficialVigilanteSettlementStaff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfficialVigilanteLGAStaff",
                table: "OfficialVigilanteLGAStaff");

            migrationBuilder.RenameTable(
                name: "OfficialVigilanteTownStaff",
                newName: "OfficialVigilanteTownAdmins");

            migrationBuilder.RenameTable(
                name: "OfficialVigilanteStateStaff",
                newName: "OfficialVigilanteStateAdmins");

            migrationBuilder.RenameTable(
                name: "OfficialVigilanteSettlementStaff",
                newName: "OfficialVigilanteSettlementAdmins");

            migrationBuilder.RenameTable(
                name: "OfficialVigilanteLGAStaff",
                newName: "OfficialVigilanteLGAAdmins");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialVigilanteTownStaff_TownId",
                table: "OfficialVigilanteTownAdmins",
                newName: "IX_OfficialVigilanteTownAdmins_TownId");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialVigilanteStateStaff_StateId",
                table: "OfficialVigilanteStateAdmins",
                newName: "IX_OfficialVigilanteStateAdmins_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialVigilanteSettlementStaff_SettlementId",
                table: "OfficialVigilanteSettlementAdmins",
                newName: "IX_OfficialVigilanteSettlementAdmins_SettlementId");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialVigilanteLGAStaff_LGAId",
                table: "OfficialVigilanteLGAAdmins",
                newName: "IX_OfficialVigilanteLGAAdmins_LGAId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfficialVigilanteTownAdmins",
                table: "OfficialVigilanteTownAdmins",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfficialVigilanteStateAdmins",
                table: "OfficialVigilanteStateAdmins",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfficialVigilanteSettlementAdmins",
                table: "OfficialVigilanteSettlementAdmins",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfficialVigilanteLGAAdmins",
                table: "OfficialVigilanteLGAAdmins",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteLGAAdmins_LGAs_LGAId",
                table: "OfficialVigilanteLGAAdmins",
                column: "LGAId",
                principalTable: "LGAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteSettlementAdmins_Settlements_SettlementId",
                table: "OfficialVigilanteSettlementAdmins",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteStateAdmins_States_StateId",
                table: "OfficialVigilanteStateAdmins",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialVigilanteTownAdmins_Towns_TownId",
                table: "OfficialVigilanteTownAdmins",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
