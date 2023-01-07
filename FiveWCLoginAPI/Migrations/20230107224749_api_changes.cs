using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiveWCLoginAPI.Migrations
{
    public partial class api_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Badges",
                table: "Registrants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Registrants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OsuGlobalRank",
                table: "Registrants",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Badges",
                table: "Registrants");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Registrants");

            migrationBuilder.DropColumn(
                name: "OsuGlobalRank",
                table: "Registrants");
        }
    }
}
