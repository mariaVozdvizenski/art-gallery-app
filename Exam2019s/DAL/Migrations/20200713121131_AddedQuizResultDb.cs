using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedQuizResultDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResult_Quizzes_QuizId",
                table: "QuizResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizResult",
                table: "QuizResult");

            migrationBuilder.RenameTable(
                name: "QuizResult",
                newName: "QuizResults");

            migrationBuilder.RenameIndex(
                name: "IX_QuizResult_QuizId",
                table: "QuizResults",
                newName: "IX_QuizResults_QuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizResults",
                table: "QuizResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResults_Quizzes_QuizId",
                table: "QuizResults",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResults_Quizzes_QuizId",
                table: "QuizResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizResults",
                table: "QuizResults");

            migrationBuilder.RenameTable(
                name: "QuizResults",
                newName: "QuizResult");

            migrationBuilder.RenameIndex(
                name: "IX_QuizResults_QuizId",
                table: "QuizResult",
                newName: "IX_QuizResult_QuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizResult",
                table: "QuizResult",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResult_Quizzes_QuizId",
                table: "QuizResult",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
