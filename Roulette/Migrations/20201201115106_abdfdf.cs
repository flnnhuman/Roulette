using Microsoft.EntityFrameworkCore.Migrations;

namespace Roulette.Migrations
{
    public partial class abdfdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "avatarhash",
                "SteamUsers",
                "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.UpdateData(
                "SteamUsers",
                "SteamID",
                "76561100000000000",
                new[]
                {
                    "avatar", "avatarfull", "avatarmedium", "communityvisibilitystate", "personaname", "profilestate",
                    "profileurl"
                },
                new object[]
                {
                    "https://cdn.cloudflare.steamstatic.com/steamcommunity/public/images/avatars/cb/.jpg",
                    "https://cdn.cloudflare.steamstatic.com/steamcommunity/public/images/avatars/cb/_full.jpg",
                    "https://cdn.cloudflare.steamstatic.com/steamcommunity/public/images/avatars/cb/_medium.jpg", 3,
                    "name", 1, "https://steamcommunity.com/id/"
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "avatarhash",
                "SteamUsers");

            migrationBuilder.UpdateData(
                "SteamUsers",
                "SteamID",
                "76561100000000000",
                new[]
                {
                    "avatar", "avatarfull", "avatarmedium", "communityvisibilitystate", "personaname", "profilestate",
                    "profileurl"
                },
                new object[] {null, null, null, 0, null, 0, null});
        }
    }
}