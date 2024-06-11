using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digital_signage_cms_backend_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schedules_players_PlayerId",
                table: "schedules");

            migrationBuilder.DropIndex(
                name: "IX_schedules_PlayerId",
                table: "schedules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_schedules_PlayerId",
                table: "schedules",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_schedules_players_PlayerId",
                table: "schedules",
                column: "PlayerId",
                principalTable: "players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
