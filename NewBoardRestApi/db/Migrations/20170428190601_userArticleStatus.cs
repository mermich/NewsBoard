using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewBoardRestApi.Migrations
{
    public partial class userArticleStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "WebSites");

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "UserFeeds",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOpened",
                table: "UserFeeds",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "UserFeeds");

            migrationBuilder.DropColumn(
                name: "IsOpened",
                table: "UserFeeds");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WebSites",
                nullable: true);
        }
    }
}
