using Microsoft.EntityFrameworkCore.Migrations;

namespace RemoteConnectionQueueApp.Data.Migrations
{
    public partial class DroppedBagliSureFromRc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaglantiSuresiDakika",
                table: "RemoteConnection");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaglantiSuresiDakika",
                table: "RemoteConnection",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
