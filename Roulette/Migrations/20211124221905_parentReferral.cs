using Microsoft.EntityFrameworkCore.Migrations;

namespace Roulette.Migrations
{
    public partial class parentReferral : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "ParentReferralId",
                table: "SteamUsers",
                type: "int unsigned",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "ReferralModels",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

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
                name: "FK_SteamUsers_ReferralModels_ParentReferralId",
                table: "SteamUsers");

            migrationBuilder.DropIndex(
                name: "IX_SteamUsers_ParentReferralId",
                table: "SteamUsers");

            migrationBuilder.DropColumn(
                name: "ParentReferralId",
                table: "SteamUsers");

            migrationBuilder.AlterColumn<float>(
                name: "Amount",
                table: "ReferralModels",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }
    }
}
