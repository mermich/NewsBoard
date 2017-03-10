using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NewBoardRestApi.Migrations
{
    public partial class feedWebsite2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WebSiteId",
                table: "Feeds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WebSites",
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
                    table.PrimaryKey("PK_WebSites", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_WebSiteId",
                table: "Feeds",
                column: "WebSiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feeds_WebSites_WebSiteId",
                table: "Feeds",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_WebSites_WebSiteId",
                table: "Feeds");

            migrationBuilder.DropTable(
                name: "WebSites");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_WebSiteId",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "WebSiteId",
                table: "Feeds");
        }
    }
}
