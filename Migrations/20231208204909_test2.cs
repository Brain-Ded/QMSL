using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMSL.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_EditableQuestions_EditableQuestionId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_GeneralQuestions_GeneralQuestionId",
                table: "Answer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.RenameTable(
                name: "Answer",
                newName: "Answers");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_GeneralQuestionId",
                table: "Answers",
                newName: "IX_Answers_GeneralQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_EditableQuestionId",
                table: "Answers",
                newName: "IX_Answers_EditableQuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_EditableQuestions_EditableQuestionId",
                table: "Answers",
                column: "EditableQuestionId",
                principalTable: "EditableQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_GeneralQuestions_GeneralQuestionId",
                table: "Answers",
                column: "GeneralQuestionId",
                principalTable: "GeneralQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_EditableQuestions_EditableQuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_GeneralQuestions_GeneralQuestionId",
                table: "Answers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.RenameTable(
                name: "Answers",
                newName: "Answer");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_GeneralQuestionId",
                table: "Answer",
                newName: "IX_Answer_GeneralQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_EditableQuestionId",
                table: "Answer",
                newName: "IX_Answer_EditableQuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_EditableQuestions_EditableQuestionId",
                table: "Answer",
                column: "EditableQuestionId",
                principalTable: "EditableQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_GeneralQuestions_GeneralQuestionId",
                table: "Answer",
                column: "GeneralQuestionId",
                principalTable: "GeneralQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
