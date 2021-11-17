using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Roulette.Migrations
{
    public partial class ndgfjd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "ChatHistory",
                table => new
                {
                    Id = table.Column<ulong>("bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SteamId = table.Column<string>("longtext CHARACTER SET utf8mb4", nullable: true),
                    Avatar = table.Column<string>("longtext CHARACTER SET utf8mb4", nullable: true),
                    Message = table.Column<string>("longtext CHARACTER SET utf8mb4", nullable: true),
                    TimeStamp = table.Column<string>("longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_ChatHistory", x => x.Id); });

            migrationBuilder.CreateTable(
                "GamesHistory",
                table => new
                {
                    Id = table.Column<ulong>("bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Timestamp = table.Column<DateTime>("datetime(6)", nullable: false),
                    WonColor = table.Column<int>("int", nullable: false),
                    WonNumber = table.Column<int>("int", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_GamesHistory", x => x.Id); });

            migrationBuilder.CreateTable(
                "BetModel",
                table => new
                {
                    Id = table.Column<ulong>("bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<float>("float", nullable: false),
                    Color = table.Column<int>("int", nullable: false),
                    ConnectionId = table.Column<string>("longtext CHARACTER SET utf8mb4", nullable: true),
                    SteamID = table.Column<string>("longtext CHARACTER SET utf8mb4", nullable: true),
                    GameModelId = table.Column<ulong>("bigint unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetModel", x => x.Id);
                    table.ForeignKey(
                        "FK_BetModel_GamesHistory_GameModelId",
                        x => x.GameModelId,
                        "GamesHistory",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_BetModel_GameModelId",
                "BetModel",
                "GameModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "BetModel");

            migrationBuilder.DropTable(
                "ChatHistory");

            migrationBuilder.DropTable(
                "GamesHistory");
        }
    }
}