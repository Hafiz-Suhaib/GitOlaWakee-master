using Microsoft.EntityFrameworkCore.Migrations;

namespace OlaWakeel.Migrations
{
    public partial class newPackagesChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "slotDate",
                table: "LawyerTimings",
                newName: "SlotDate");

            migrationBuilder.AddColumn<string>(
                name: "EndTime24",
                table: "LawyerTimings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InternationalIndex",
                table: "LawyerTimings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocalIndex",
                table: "LawyerTimings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StartTime24",
                table: "LawyerTimings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime24",
                table: "LawyerTimings");

            migrationBuilder.DropColumn(
                name: "InternationalIndex",
                table: "LawyerTimings");

            migrationBuilder.DropColumn(
                name: "LocalIndex",
                table: "LawyerTimings");

            migrationBuilder.DropColumn(
                name: "StartTime24",
                table: "LawyerTimings");

            migrationBuilder.RenameColumn(
                name: "SlotDate",
                table: "LawyerTimings",
                newName: "slotDate");
        }
    }
}
