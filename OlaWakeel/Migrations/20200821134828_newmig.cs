﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace OlaWakeel.Migrations
{
    public partial class newmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LawyerCaseCategoryId",
                table: "Appointments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_LawyerCaseCategoryId",
                table: "Appointments",
                column: "LawyerCaseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_LawyerCaseCategories_LawyerCaseCategoryId",
                table: "Appointments",
                column: "LawyerCaseCategoryId",
                principalTable: "LawyerCaseCategories",
                principalColumn: "LawyerCaseCategoryId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_LawyerCaseCategories_LawyerCaseCategoryId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_LawyerCaseCategoryId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "LawyerCaseCategoryId",
                table: "Appointments");
        }
    }
}
