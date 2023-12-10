using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMSL.Migrations
{
    /// <inheritdoc />
    public partial class Test4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_EditableQuestions_EditableQuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_GeneralQuestions_GeneralQuestionId",
                table: "Answers");

            migrationBuilder.AlterColumn<int>(
                name: "GeneralQuestionId",
                table: "Answers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EditableQuestionId",
                table: "Answers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_EditableQuestions_EditableQuestionId",
                table: "Answers",
                column: "EditableQuestionId",
                principalTable: "EditableQuestions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_GeneralQuestions_GeneralQuestionId",
                table: "Answers",
                column: "GeneralQuestionId",
                principalTable: "GeneralQuestions",
                principalColumn: "Id");
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

            migrationBuilder.AlterColumn<int>(
                name: "GeneralQuestionId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EditableQuestionId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
