using Microsoft.EntityFrameworkCore.Migrations;

namespace RemoteConnectionQueueApp.Data.Migrations
{
    public partial class ChangedNotToNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Not",
                table: "RemoteConnection");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "RemoteConnection",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "RemoteConnection");

            migrationBuilder.AddColumn<string>(
                name: "Not",
                table: "RemoteConnection",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
