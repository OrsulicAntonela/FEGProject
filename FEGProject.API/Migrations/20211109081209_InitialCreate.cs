using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FEGProject.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    ExamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.ExamId);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "AssignedExams",
                columns: table => new
                {
                    AssignedExamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedExams", x => x.AssignedExamId);
                    table.ForeignKey(
                        name: "FK_AssignedExams_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adderss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamQuestion",
                columns: table => new
                {
                    ExamsExamId = table.Column<int>(type: "int", nullable: false),
                    QuestionsQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuestion", x => new { x.ExamsExamId, x.QuestionsQuestionId });
                    table.ForeignKey(
                        name: "FK_ExamQuestion_Exams_ExamsExamId",
                        column: x => x.ExamsExamId,
                        principalTable: "Exams",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamQuestion_Questions_QuestionsQuestionId",
                        column: x => x.QuestionsQuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                columns: table => new
                {
                    QuestionOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.QuestionOptionId);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignedExamGroup",
                columns: table => new
                {
                    AssignedExamsAssignedExamId = table.Column<int>(type: "int", nullable: false),
                    GroupsGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedExamGroup", x => new { x.AssignedExamsAssignedExamId, x.GroupsGroupId });
                    table.ForeignKey(
                        name: "FK_AssignedExamGroup_AssignedExams_AssignedExamsAssignedExamId",
                        column: x => x.AssignedExamsAssignedExamId,
                        principalTable: "AssignedExams",
                        principalColumn: "AssignedExamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedExamGroup_Groups_GroupsGroupId",
                        column: x => x.GroupsGroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAssignedExams",
                columns: table => new
                {
                    StudentAssignedExamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Result = table.Column<double>(type: "float", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    AssignedExamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssignedExams", x => x.StudentAssignedExamId);
                    table.ForeignKey(
                        name: "FK_StudentAssignedExams_AssignedExams_AssignedExamId",
                        column: x => x.AssignedExamId,
                        principalTable: "AssignedExams",
                        principalColumn: "AssignedExamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAssignedExams_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAnswers",
                columns: table => new
                {
                    StudentAnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentAssignedExamId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAnswers", x => x.StudentAnswerId);
                    table.ForeignKey(
                        name: "FK_StudentAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAnswers_StudentAssignedExams_StudentAssignedExamId",
                        column: x => x.StudentAssignedExamId,
                        principalTable: "StudentAssignedExams",
                        principalColumn: "StudentAssignedExamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedExamGroup_GroupsGroupId",
                table: "AssignedExamGroup",
                column: "GroupsGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedExams_ExamId",
                table: "AssignedExams",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestion_QuestionsQuestionId",
                table: "ExamQuestion",
                column: "QuestionsQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_QuestionId",
                table: "StudentAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_StudentAssignedExamId",
                table: "StudentAnswers",
                column: "StudentAssignedExamId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignedExams_AssignedExamId",
                table: "StudentAssignedExams",
                column: "AssignedExamId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignedExams_StudentId",
                table: "StudentAssignedExams",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedExamGroup");

            migrationBuilder.DropTable(
                name: "ExamQuestion");

            migrationBuilder.DropTable(
                name: "QuestionOptions");

            migrationBuilder.DropTable(
                name: "StudentAnswers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "StudentAssignedExams");

            migrationBuilder.DropTable(
                name: "AssignedExams");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
