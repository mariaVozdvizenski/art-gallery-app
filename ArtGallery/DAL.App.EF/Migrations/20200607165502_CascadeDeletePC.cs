using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class CascadeDeletePC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaintingCategories_Paintings_PaintingId",
                table: "PaintingCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_PaintingCategories_Paintings_PaintingId",
                table: "PaintingCategories",
                column: "PaintingId",
                principalTable: "Paintings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaintingCategories_Paintings_PaintingId",
                table: "PaintingCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_PaintingCategories_Paintings_PaintingId",
                table: "PaintingCategories",
                column: "PaintingId",
                principalTable: "Paintings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
