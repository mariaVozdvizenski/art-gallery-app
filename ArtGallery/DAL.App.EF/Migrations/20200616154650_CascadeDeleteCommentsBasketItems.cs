using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class CascadeDeleteCommentsBasketItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Paintings_PaintingId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Paintings_PaintingId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Paintings_PaintingId",
                table: "BasketItems",
                column: "PaintingId",
                principalTable: "Paintings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Paintings_PaintingId",
                table: "Comments",
                column: "PaintingId",
                principalTable: "Paintings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Paintings_PaintingId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Paintings_PaintingId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Paintings_PaintingId",
                table: "BasketItems",
                column: "PaintingId",
                principalTable: "Paintings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Paintings_PaintingId",
                table: "Comments",
                column: "PaintingId",
                principalTable: "Paintings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
