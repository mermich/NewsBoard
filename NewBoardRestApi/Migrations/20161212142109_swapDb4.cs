using Microsoft.EntityFrameworkCore.Migrations;

namespace NewBoardRestApi.Migrations
{
    public partial class swapDb4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Label2",
                table: "Tags",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Label2",
                table: "Tags");
        }
    }
}
