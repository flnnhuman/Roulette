using Microsoft.EntityFrameworkCore.Migrations;

namespace Roulette.Migrations
{
    public partial class nfejs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "ChatHistory");

            migrationBuilder.DropColumn(
                name: "SteamId",
                table: "ChatHistory");

            migrationBuilder.AddColumn<string>(
                name: "UserSteamID",
                table: "ChatHistory",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatHistory_UserSteamID",
                table: "ChatHistory",
                column: "UserSteamID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatHistory_SteamUsers_UserSteamID",
                table: "ChatHistory",
                column: "UserSteamID",
                principalTable: "SteamUsers",
                principalColumn: "SteamID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatHistory_SteamUsers_UserSteamID",
                table: "ChatHistory");

            migrationBuilder.DropIndex(
                name: "IX_ChatHistory_UserSteamID",
                table: "ChatHistory");

            migrationBuilder.DropColumn(
                name: "UserSteamID",
                table: "ChatHistory");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "ChatHistory",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SteamId",
                table: "ChatHistory",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
