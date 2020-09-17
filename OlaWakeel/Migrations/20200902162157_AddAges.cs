using Microsoft.EntityFrameworkCore.Migrations;

namespace OlaWakeel.Migrations
{
    public partial class AddAges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
               name: "Age",
               table: "Lawyers",
               nullable: false,
               defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "Age",
               table: "Lawyers");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Customers");
        }
    }
}
