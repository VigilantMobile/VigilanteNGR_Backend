using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedvotingandcommentsfunctionalityforreportedalerts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentFlags_Comments_CommentId",
                table: "CommentFlags");

            migrationBuilder.DropIndex(
                name: "IX_CommentFlags_CommentId",
                table: "CommentFlags");

            migrationBuilder.AddColumn<bool>(
                name: "IsInviteeEmergencyContact",
                table: "UserCircle",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInviterEmergencyContact",
                table: "UserCircle",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RelationshipType",
                table: "UserCircle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DownvoteCount",
                table: "SecurityTips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpvoteCount",
                table: "SecurityTips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommentVote",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DownvoteCount",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpvoteCount",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SecurityTipVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SecurityTipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityTipVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityTipVotes_SecurityTips_SecurityTipId",
                        column: x => x.SecurityTipId,
                        principalTable: "SecurityTips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityTipVotes_Users_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentFlags_CommentId_VoterId",
                table: "CommentFlags",
                columns: new[] { "CommentId", "VoterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTipVotes_SecurityTipId_VoterId",
                table: "SecurityTipVotes",
                columns: new[] { "SecurityTipId", "VoterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecurityTipVotes_VoterId",
                table: "SecurityTipVotes",
                column: "VoterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentFlags_Comments_CommentId",
                table: "CommentFlags",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentFlags_Comments_CommentId",
                table: "CommentFlags");

            migrationBuilder.DropTable(
                name: "SecurityTipVotes");

            migrationBuilder.DropIndex(
                name: "IX_CommentFlags_CommentId_VoterId",
                table: "CommentFlags");

            migrationBuilder.DropColumn(
                name: "IsInviteeEmergencyContact",
                table: "UserCircle");

            migrationBuilder.DropColumn(
                name: "IsInviterEmergencyContact",
                table: "UserCircle");

            migrationBuilder.DropColumn(
                name: "RelationshipType",
                table: "UserCircle");

            migrationBuilder.DropColumn(
                name: "DownvoteCount",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "UpvoteCount",
                table: "SecurityTips");

            migrationBuilder.DropColumn(
                name: "CommentVote",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "DownvoteCount",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UpvoteCount",
                table: "Comments");

            migrationBuilder.CreateIndex(
                name: "IX_CommentFlags_CommentId",
                table: "CommentFlags",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentFlags_Comments_CommentId",
                table: "CommentFlags",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
