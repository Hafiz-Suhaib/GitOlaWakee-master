using Microsoft.EntityFrameworkCore.Migrations;

namespace OlaWakeel.Migrations
{
    public partial class newChangeInLawyer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirbaseToken",
                table: "Lawyers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirbaseToken",
                table: "Lawyers");
        }
    }
}
