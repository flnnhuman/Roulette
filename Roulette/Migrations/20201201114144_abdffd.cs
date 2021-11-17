using Microsoft.EntityFrameworkCore.Migrations;

namespace Roulette.Migrations
{
    public partial class abdffd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "avatar",
                "SteamUsers",
                "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "avatarfull",
                "SteamUsers",
                "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "avatarmedium",
                "SteamUsers",
                "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "communityvisibilitystate",
                "SteamUsers",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                "personaname",
                "SteamUsers",
                "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "personastate",
                "SteamUsers",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "personastateflags",
                "SteamUsers",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "profilestate",
                "SteamUsers",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                "profileurl",
                "SteamUsers",
                "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "avatar",
                "SteamUsers");

            migrationBuilder.DropColumn(
                "avatarfull",
                "SteamUsers");

            migrationBuilder.DropColumn(
                "avatarmedium",
                "SteamUsers");

            migrationBuilder.DropColumn(
                "communityvisibilitystate",
                "SteamUsers");

            migrationBuilder.DropColumn(
                "personaname",
                "SteamUsers");

            migrationBuilder.DropColumn(
                "personastate",
                "SteamUsers");

            migrationBuilder.DropColumn(
                "personastateflags",
                "SteamUsers");

            migrationBuilder.DropColumn(
                "profilestate",
                "SteamUsers");

            migrationBuilder.DropColumn(
                "profileurl",
                "SteamUsers");
        }
    }
}