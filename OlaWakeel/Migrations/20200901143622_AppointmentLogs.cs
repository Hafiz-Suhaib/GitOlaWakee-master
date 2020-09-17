using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OlaWakeel.Migrations
{
    public partial class AppointmentLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Log_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Appointment_Id = table.Column<int>(nullable: false),
                    AppointmentAppoinmentId = table.Column<int>(nullable: true),
                    User_id = table.Column<int>(nullable: false),
                    User_Type = table.Column<string>(nullable: true),
                    Log_Decs = table.Column<string>(nullable: true),
                    Log_Status = table.Column<string>(nullable: true),
                    LogDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Log_Id);
                    table.ForeignKey(
                        name: "FK_Logs_Appointments_AppointmentAppoinmentId",
                        column: x => x.AppointmentAppoinmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppoinmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_AppointmentAppoinmentId",
                table: "Logs",
                column: "AppointmentAppoinmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
