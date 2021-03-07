using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Migrations
{
    public partial class AddDeactivatedFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deactivated",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deactivated",
                table: "User");
        }
    }
}
