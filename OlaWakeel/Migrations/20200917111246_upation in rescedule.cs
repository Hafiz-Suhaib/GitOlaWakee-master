using Microsoft.EntityFrameworkCore.Migrations;

namespace OlaWakeel.Migrations
{
    public partial class upationinrescedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDay",
                table: "RescheduleAppoints");

            migrationBuilder.AddColumn<float>(
                name: "CaseCharges",
                table: "RescheduleAppoints",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseCharges",
                table: "RescheduleAppoints");

            migrationBuilder.AddColumn<string>(
                name: "AppointmentDay",
                table: "RescheduleAppoints",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
