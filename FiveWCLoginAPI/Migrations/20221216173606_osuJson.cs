using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiveWCLoginAPI.Migrations
{
    public partial class osuJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OsuJson",
                table: "Registrants",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OsuJson",
                table: "Registrants");
        }
    }
}
