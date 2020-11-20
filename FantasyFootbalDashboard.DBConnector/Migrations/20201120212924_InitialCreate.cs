using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyFootbalDashboard.DBConnector.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReferencePlayers",
                columns: table => new
                {
                    ReferencePlayerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    Team = table.Column<int>(nullable: false),
                    PlayerPhotoUrl = table.Column<string>(nullable: true),
                    SportsDataIoPlayerId = table.Column<int>(nullable: true),
                    EspnPlayerId = table.Column<int>(nullable: true),
                    MyFantasyLeagePlayerId = table.Column<int>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferencePlayers", x => x.ReferencePlayerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReferencePlayers");
        }
    }
}
