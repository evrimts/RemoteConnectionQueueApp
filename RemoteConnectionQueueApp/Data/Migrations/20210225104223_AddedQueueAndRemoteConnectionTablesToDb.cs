using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RemoteConnectionQueueApp.Data.Migrations
{
    public partial class AddedQueueAndRemoteConnectionTablesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RemoteConnection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriAdi = table.Column<string>(maxLength: 120, nullable: false),
                    SunucuIp = table.Column<string>(maxLength: 60, nullable: true),
                    SunucuKullaniciAdi = table.Column<string>(maxLength: 60, nullable: true),
                    BagliKisi = table.Column<string>(maxLength: 120, nullable: true),
                    BaglantiZamani = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteConnection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Queue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BekleyenKisi = table.Column<string>(nullable: true),
                    RemoteConnectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Queue_RemoteConnection_RemoteConnectionId",
                        column: x => x.RemoteConnectionId,
                        principalTable: "RemoteConnection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Queue_RemoteConnectionId",
                table: "Queue",
                column: "RemoteConnectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Queue");

            migrationBuilder.DropTable(
                name: "RemoteConnection");
        }
    }
}
