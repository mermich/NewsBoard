using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NewBoardRestApi.Migrations
{
    public partial class tags1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Label2",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Feeds",
                newName: "WebSiteUrl");

            migrationBuilder.RenameColumn(
                name: "Label",
                table: "Feeds",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "SyndicationUrl",
                table: "Feeds",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FeedTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FeedId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedTag_Feeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedTag_FeedId",
                table: "FeedTag",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedTag_TagId",
                table: "FeedTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedTag");

            migrationBuilder.DropColumn(
                name: "SyndicationUrl",
                table: "Feeds");

            migrationBuilder.RenameColumn(
                name: "WebSiteUrl",
                table: "Feeds",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Feeds",
                newName: "Label");

            migrationBuilder.AddColumn<string>(
                name: "Label2",
                table: "Tags",
                nullable: true);
        }
    }
}
