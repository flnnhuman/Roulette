using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Roulette.Migrations
{
    public partial class ghafdjhhgfjhg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SteamUsers",
                keyColumn: "SteamID",
                keyValue: "76561100000000000");

            migrationBuilder.CreateTable(
                name: "ReferralModels",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    Usages = table.Column<uint>(type: "int unsigned", nullable: false),
                    SteamUsersId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferralModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferralModels_SteamUsers_SteamUsersId",
                        column: x => x.SteamUsersId,
                        principalTable: "SteamUsers",
                        principalColumn: "SteamID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReferralModels_SteamUsersId",
                table: "ReferralModels",
                column: "SteamUsersId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReferralModels");

            migrationBuilder.InsertData(
                table: "SteamUsers",
                columns: new[] { "SteamID", "Balance", "TotalDeposited", "TotalWon", "TradeLink", "avatar", "avatarfull", "avatarhash", "avatarmedium", "communityvisibilitystate", "personaname", "personastate", "personastateflags", "profilestate", "profileurl" },
                values: new object[] { "76561100000000000", 0.0, 0f, 0f, null, "https://cdn.cloudflare.steamstatic.com/steamcommunity/public/images/avatars/cb/.jpg", "https://cdn.cloudflare.steamstatic.com/steamcommunity/public/images/avatars/cb/_full.jpg", null, "https://cdn.cloudflare.steamstatic.com/steamcommunity/public/images/avatars/cb/_medium.jpg", 3, "name", 0, 0, 1, "https://steamcommunity.com/id/" });
        }
    }
}
