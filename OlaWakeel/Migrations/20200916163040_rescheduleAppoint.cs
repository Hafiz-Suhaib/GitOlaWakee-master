using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OlaWakeel.Migrations
{
    public partial class rescheduleAppoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RescheduleAppoints",
                columns: table => new
                {
                    RescheduleAppoint_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(nullable: false),
                    RescheduleDate = table.Column<DateTime>(nullable: false),
                    TimeTo = table.Column<string>(nullable: true),
                    TimeFrom = table.Column<string>(nullable: true),
                    AppointmentDay = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RescheduleAppoints", x => x.RescheduleAppoint_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RescheduleAppoints");
        }
    }
}
