using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digital_signage_cms_backend_api.Migrations
{
    /// <inheritdoc />
    public partial class AddSchedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_players_PlayerId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_playlists_PlaylistId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "player_playlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "schedules");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_PlaylistId",
                table: "schedules",
                newName: "IX_schedules_PlaylistId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_PlayerId",
                table: "schedules",
                newName: "IX_schedules_PlayerId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "schedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "DaysOfWeek",
                table: "schedules",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "schedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "content_items",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_schedules",
                table: "schedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_schedules_players_PlayerId",
                table: "schedules",
                column: "PlayerId",
                principalTable: "players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_schedules_playlists_PlaylistId",
                table: "schedules",
                column: "PlaylistId",
                principalTable: "playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schedules_players_PlayerId",
                table: "schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_schedules_playlists_PlaylistId",
                table: "schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_schedules",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "content_items");

            migrationBuilder.RenameTable(
                name: "schedules",
                newName: "Schedules");

            migrationBuilder.RenameIndex(
                name: "IX_schedules_PlaylistId",
                table: "Schedules",
                newName: "IX_Schedules_PlaylistId");

            migrationBuilder.RenameIndex(
                name: "IX_schedules_PlayerId",
                table: "Schedules",
                newName: "IX_Schedules_PlayerId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Schedules",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "DaysOfWeek",
                table: "Schedules",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Schedules",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "player_playlists",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    PlaylistId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_playlists", x => new { x.PlayerId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_player_playlists_players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_player_playlists_playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_player_playlists_PlaylistId",
                table: "player_playlists",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_players_PlayerId",
                table: "Schedules",
                column: "PlayerId",
                principalTable: "players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_playlists_PlaylistId",
                table: "Schedules",
                column: "PlaylistId",
                principalTable: "playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
