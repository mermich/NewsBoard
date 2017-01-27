using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NewBoardRestApi.Migrations
{
    public partial class website : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebSiteUrl",
                table: "Feeds");

            migrationBuilder.AddColumn<DateTime>(
                name: "OpenedTime",
                table: "UserArticles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "WebSiteId",
                table: "Feeds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WebSite",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    IconUrl = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSite", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_WebSiteId",
                table: "Feeds",
                column: "WebSiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feeds_WebSite_WebSiteId",
                table: "Feeds",
                column: "WebSiteId",
                principalTable: "WebSite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_WebSite_WebSiteId",
                table: "Feeds");

            migrationBuilder.DropTable(
                name: "WebSite");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_WebSiteId",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "OpenedTime",
                table: "UserArticles");

            migrationBuilder.DropColumn(
                name: "WebSiteId",
                table: "Feeds");

            migrationBuilder.AddColumn<string>(
                name: "WebSiteUrl",
                table: "Feeds",
                nullable: true);
        }
    }
}
