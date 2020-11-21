using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FantasyFootbalDashboard.DBConnector.Migrations
{
    public partial class _11212020_AddMoreSportsDataCols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ReferencePlayerId",
                table: "ReferencePlayers",
                defaultValueSql: "newid()");

            migrationBuilder.AddColumn<double>(
                name: "AverageDraftPosition",
                table: "ReferencePlayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ByeWeek",
                table: "ReferencePlayers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "College",
                table: "ReferencePlayers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JerseyNumber",
                table: "ReferencePlayers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ReferencePlayers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearsInLeague",
                table: "ReferencePlayers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageDraftPosition",
                table: "ReferencePlayers");

            migrationBuilder.DropColumn(
                name: "ByeWeek",
                table: "ReferencePlayers");

            migrationBuilder.DropColumn(
                name: "College",
                table: "ReferencePlayers");

            migrationBuilder.DropColumn(
                name: "JerseyNumber",
                table: "ReferencePlayers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ReferencePlayers");

            migrationBuilder.DropColumn(
                name: "YearsInLeague",
                table: "ReferencePlayers");
        }
    }
}
