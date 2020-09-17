using Microsoft.EntityFrameworkCore.Migrations;

namespace OlaWakeel.Migrations
{
    public partial class NewFavourite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavouriteLawyers",
                columns: table => new
                {
                    FavouriteLawyerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    LawyerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteLawyers", x => x.FavouriteLawyerId);
                    table.ForeignKey(
                        name: "FK_FavouriteLawyers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavouriteLawyers_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "LawyerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteLawyers_CustomerId",
                table: "FavouriteLawyers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteLawyers_LawyerId",
                table: "FavouriteLawyers",
                column: "LawyerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteLawyers");
        }
    }
}
