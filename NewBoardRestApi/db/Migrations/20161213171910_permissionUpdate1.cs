using Microsoft.EntityFrameworkCore.Migrations;

namespace NewBoardRestApi.Migrations
{
    public partial class permissionUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Label",
                table: "Users",
                newName: "UserName");

            //migrationBuilder.RenameColumn(
            //    name: "Title",
            //    table: "Feeds",
            //    newName: "Label");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Label");

            migrationBuilder.RenameColumn(
                name: "Label",
                table: "Feeds",
                newName: "Title");
        }
    }
}
