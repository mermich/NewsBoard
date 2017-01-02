using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewBoardRestApi.Migrations
{
    public partial class tags3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedTag_Feeds_FeedId",
                table: "FeedTag");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedTag_Tags_TagId",
                table: "FeedTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedTag",
                table: "FeedTag");

            migrationBuilder.RenameTable(
                name: "FeedTag",
                newName: "FeedTags");

            migrationBuilder.RenameIndex(
                name: "IX_FeedTag_TagId",
                table: "FeedTags",
                newName: "IX_FeedTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_FeedTag_FeedId",
                table: "FeedTags",
                newName: "IX_FeedTags_FeedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedTags",
                table: "FeedTags",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedTags_Feeds_FeedId",
                table: "FeedTags",
                column: "FeedId",
                principalTable: "Feeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedTags_Tags_TagId",
                table: "FeedTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedTags_Feeds_FeedId",
                table: "FeedTags");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedTags_Tags_TagId",
                table: "FeedTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedTags",
                table: "FeedTags");

            migrationBuilder.RenameTable(
                name: "FeedTags",
                newName: "FeedTag");

            migrationBuilder.RenameIndex(
                name: "IX_FeedTags_TagId",
                table: "FeedTag",
                newName: "IX_FeedTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_FeedTags_FeedId",
                table: "FeedTag",
                newName: "IX_FeedTag_FeedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedTag",
                table: "FeedTag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedTag_Feeds_FeedId",
                table: "FeedTag",
                column: "FeedId",
                principalTable: "Feeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedTag_Tags_TagId",
                table: "FeedTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
