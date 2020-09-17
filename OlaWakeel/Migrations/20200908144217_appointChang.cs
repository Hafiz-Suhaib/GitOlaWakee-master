using Microsoft.EntityFrameworkCore.Migrations;

namespace OlaWakeel.Migrations
{
    public partial class appointChang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_LawyerCaseCategories_LawyerCaseCategoryId",
                table: "Appointments");

            //migrationBuilder.DropIndex(
            //    name: "IX_Appointments_LawyerCaseCategoryId",
            //    table: "Appointments");

            migrationBuilder.DropColumn(
                name: "LawyerCaseCategoryId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "CaseCategoryId",
                table: "Appointments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CaseCategoryId",
                table: "Appointments",
                column: "CaseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_CaseCategories_CaseCategoryId",
                table: "Appointments",
                column: "CaseCategoryId",
                principalTable: "CaseCategories",
                principalColumn: "CaseCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_CaseCategories_CaseCategoryId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CaseCategoryId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CaseCategoryId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "LawyerCaseCategoryId",
                table: "Appointments",
                type: "int",
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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
