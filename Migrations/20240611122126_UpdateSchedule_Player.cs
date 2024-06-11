using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digital_signage_cms_backend_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchedule_Player : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "schedules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "schedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
