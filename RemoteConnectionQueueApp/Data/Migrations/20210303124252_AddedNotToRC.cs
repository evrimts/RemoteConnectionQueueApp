using Microsoft.EntityFrameworkCore.Migrations;

namespace RemoteConnectionQueueApp.Data.Migrations
{
    public partial class AddedNotToRC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Not",
                table: "RemoteConnection",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Not",
                table: "RemoteConnection");
        }
    }
}
