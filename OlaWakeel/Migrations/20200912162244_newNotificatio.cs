using Microsoft.EntityFrameworkCore.Migrations;

namespace OlaWakeel.Migrations
{
    public partial class newNotificatio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Usertype",
                table: "Notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usertype",
                table: "Notifications");
        }
    }
}
