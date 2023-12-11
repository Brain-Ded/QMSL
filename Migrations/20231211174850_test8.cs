using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMSL.Migrations
{
    /// <inheritdoc />
    public partial class test8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.AddColumn<string>(
                name: "Disease",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EditableAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditableQuestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditableAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EditableAnswers_EditableQuestions_EditableQuestionId",
                        column: x => x.EditableQuestionId,
                        principalTable: "EditableQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GeneralAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralQuestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralAnswers_GeneralQuestions_GeneralQuestionId",
                        column: x => x.GeneralQuestionId,
                        principalTable: "GeneralQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EditableAnswers_EditableQuestionId",
                table: "EditableAnswers",
                column: "EditableQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralAnswers_GeneralQuestionId",
                table: "GeneralAnswers",
                column: "GeneralQuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditableAnswers");

            migrationBuilder.DropTable(
                name: "GeneralAnswers");

            migrationBuilder.DropColumn(
                name: "Disease",
                table: "Patients");

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EditableQuestionId = table.Column<int>(type: "int", nullable: true),
                    GeneralQuestionId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_EditableQuestions_EditableQuestionId",
                        column: x => x.EditableQuestionId,
                        principalTable: "EditableQuestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Answers_GeneralQuestions_GeneralQuestionId",
                        column: x => x.GeneralQuestionId,
                        principalTable: "GeneralQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_EditableQuestionId",
                table: "Answers",
                column: "EditableQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_GeneralQuestionId",
                table: "Answers",
                column: "GeneralQuestionId");
        }
    }
}
