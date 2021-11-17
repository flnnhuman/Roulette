using Microsoft.EntityFrameworkCore.Migrations;

namespace Roulette.Migrations
{
    public partial class abdfd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "SteamUsers",
                "SteamID",
                "76561198181370493");

            migrationBuilder.AddColumn<double>(
                "Balance",
                "SteamUsers",
                "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                "SteamUsers",
                new[] {"SteamID", "Balance"},
                new object[] {"76561100000000000", 0.0});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "SteamUsers",
                "SteamID",
                "76561100000000000");

            migrationBuilder.DropColumn(
                "Balance",
                "SteamUsers");

            migrationBuilder.InsertData(
                "SteamUsers",
                "SteamID",
                "76561198181370493");
        }
    }
}