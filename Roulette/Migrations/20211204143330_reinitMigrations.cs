using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Roulette.Migrations
{
    public partial class reinitMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatHistory",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SteamId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Avatar = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PersonaName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Message = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GamesHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    WonColor = table.Column<int>(type: "int", nullable: false),
                    WonNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BetModel",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    ConnectionId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    SteamID = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    GameModelId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BetModel_GamesHistory_GameModelId",
                        column: x => x.GameModelId,
                        principalTable: "GamesHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SteamUsers",
                columns: table => new
                {
                    SteamID = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Balance = table.Column<double>(type: "double", nullable: false),
                    communityvisibilitystate = table.Column<int>(type: "int", nullable: false),
                    profilestate = table.Column<int>(type: "int", nullable: false),
                    personastate = table.Column<int>(type: "int", nullable: false),
                    personastateflags = table.Column<int>(type: "int", nullable: false),
                    personaname = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    profileurl = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    avatar = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    avatarhash = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    avatarmedium = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    avatarfull = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    TradeLink = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    TotalWon = table.Column<float>(type: "float", nullable: false),
                    TotalDeposited = table.Column<float>(type: "float", nullable: false),
                    ParentReferralId = table.Column<uint>(type: "int unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamUsers", x => x.SteamID);
                });

            migrationBuilder.CreateTable(
                name: "ReferralModels",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Amount = table.Column<double>(type: "double", nullable: false),
                    Usages = table.Column<uint>(type: "int unsigned", nullable: false),
                    TotalProfit = table.Column<uint>(type: "int unsigned", nullable: false),
                    TotalBets = table.Column<ulong>(type: "bigint unsigned", nullable: false),
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
                name: "IX_BetModel_GameModelId",
                table: "BetModel",
                column: "GameModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferralModels_SteamUsersId",
                table: "ReferralModels",
                column: "SteamUsersId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SteamUsers_ParentReferralId",
                table: "SteamUsers",
                column: "ParentReferralId");

            migrationBuilder.AddForeignKey(
                name: "FK_SteamUsers_ReferralModels_ParentReferralId",
                table: "SteamUsers",
                column: "ParentReferralId",
                principalTable: "ReferralModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferralModels_SteamUsers_SteamUsersId",
                table: "ReferralModels");

            migrationBuilder.DropTable(
                name: "BetModel");

            migrationBuilder.DropTable(
                name: "ChatHistory");

            migrationBuilder.DropTable(
                name: "GamesHistory");

            migrationBuilder.DropTable(
                name: "SteamUsers");

            migrationBuilder.DropTable(
                name: "ReferralModels");
        }
    }
}
