using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chain.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAttachmentModel_AddMimeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageMimeType",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageMimeType",
                table: "Attachments");
        }
    }
}
