using Microsoft.EntityFrameworkCore.Migrations;

namespace Roulette.Migrations
{
    public partial class ajfdh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                "TotalDeposited",
                "SteamUsers",
                "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                "TotalWon",
                "SteamUsers",
                "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                "TradeLink",
                "SteamUsers",
                "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "TotalDeposited",
                "SteamUsers");

            migrationBuilder.DropColumn(
                "TotalWon",
                "SteamUsers");

            migrationBuilder.DropColumn(
                "TradeLink",
                "SteamUsers");
        }
    }
}