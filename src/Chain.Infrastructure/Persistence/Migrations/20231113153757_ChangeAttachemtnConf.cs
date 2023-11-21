using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chain.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAttachemtnConf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Attachments",
                newName: "Attachment");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Attachment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Attachment");

            migrationBuilder.RenameTable(
                name: "Attachment",
                newName: "Attachments");
        }
    }
}
