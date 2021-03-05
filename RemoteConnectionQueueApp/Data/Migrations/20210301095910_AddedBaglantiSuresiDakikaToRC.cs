using Microsoft.EntityFrameworkCore.Migrations;

namespace RemoteConnectionQueueApp.Data.Migrations
{
    public partial class AddedBaglantiSuresiDakikaToRC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaglantiSuresiDakika",
                table: "RemoteConnection",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaglantiSuresiDakika",
                table: "RemoteConnection");
        }
    }
}
