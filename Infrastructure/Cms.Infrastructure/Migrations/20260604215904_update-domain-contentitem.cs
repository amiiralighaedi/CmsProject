using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedomaincontentitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "ContentVersions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedAt",
                table: "ContentVersions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "ContentItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ContentItems_ContentTypeId",
                table: "ContentItems",
                column: "ContentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentItems_ContentTypes_ContentTypeId",
                table: "ContentItems",
                column: "ContentTypeId",
                principalTable: "ContentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentItems_ContentTypes_ContentTypeId",
                table: "ContentItems");

            migrationBuilder.DropIndex(
                name: "IX_ContentItems_ContentTypeId",
                table: "ContentItems");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "ContentVersions");

            migrationBuilder.DropColumn(
                name: "PublishedAt",
                table: "ContentVersions");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "ContentItems");
        }
    }
}
