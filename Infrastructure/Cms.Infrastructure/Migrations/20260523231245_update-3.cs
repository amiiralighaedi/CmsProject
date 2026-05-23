using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Values",
                table: "ContentVersions");

            migrationBuilder.DropColumn(
                name: "FieldDefinitions",
                table: "ContentTypes");

            migrationBuilder.DropColumn(
                name: "Values",
                table: "ContentItems");

            migrationBuilder.CreateTable(
                name: "ContentFieldDefinition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    ContentTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentFieldDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentFieldDefinition_ContentTypes_ContentTypeId",
                        column: x => x.ContentTypeId,
                        principalTable: "ContentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentItems_Values",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    ContentItemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentItems_Values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentItems_Values_ContentItems_ContentItemId",
                        column: x => x.ContentItemId,
                        principalTable: "ContentItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentVersions_Values",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    ContentVersionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentVersions_Values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentVersions_Values_ContentVersions_ContentVersionId",
                        column: x => x.ContentVersionId,
                        principalTable: "ContentVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentFieldDefinition_ContentTypeId",
                table: "ContentFieldDefinition",
                column: "ContentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentItems_Values_ContentItemId",
                table: "ContentItems_Values",
                column: "ContentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentVersions_Values_ContentVersionId",
                table: "ContentVersions_Values",
                column: "ContentVersionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentFieldDefinition");

            migrationBuilder.DropTable(
                name: "ContentItems_Values");

            migrationBuilder.DropTable(
                name: "ContentVersions_Values");

            migrationBuilder.AddColumn<string>(
                name: "Values",
                table: "ContentVersions",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldDefinitions",
                table: "ContentTypes",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Values",
                table: "ContentItems",
                type: "jsonb",
                nullable: true);
        }
    }
}
