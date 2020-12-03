using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyFootbalDashboard.DBConnector.Migrations
{
    public partial class CreateGamesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NflGame",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    SeasonYear = table.Column<int>(nullable: false),
                    AwayTeam = table.Column<int>(nullable: false),
                    HomeTeam = table.Column<int>(nullable: false),
                    Week = table.Column<int>(nullable: false),
                    NetworkChannel = table.Column<string>(nullable: true),
                    GameDateTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameId", x => x.GameId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NflGame");
        }
    }
}
