using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digital_signage_cms_backend_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContentItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailUrl",
                table: "content_items");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "content_items",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "content_items",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "content_items");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "content_items");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailUrl",
                table: "content_items",
                type: "text",
                nullable: true);
        }
    }
}
