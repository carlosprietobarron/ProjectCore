using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameTeacherTeacherToCourseTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherTeacher_Course_CourseId",
                table: "TeacherTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherTeacher_Teacher_TeacherId",
                table: "TeacherTeacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherTeacher",
                table: "TeacherTeacher");

            migrationBuilder.RenameTable(
                name: "TeacherTeacher",
                newName: "CourseTeacher");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherTeacher_CourseId",
                table: "CourseTeacher",
                newName: "IX_CourseTeacher_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseTeacher",
                table: "CourseTeacher",
                columns: new[] { "TeacherId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTeacher_Course_CourseId",
                table: "CourseTeacher",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTeacher_Teacher_TeacherId",
                table: "CourseTeacher",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTeacher_Course_CourseId",
                table: "CourseTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTeacher_Teacher_TeacherId",
                table: "CourseTeacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseTeacher",
                table: "CourseTeacher");

            migrationBuilder.RenameTable(
                name: "CourseTeacher",
                newName: "TeacherTeacher");

            migrationBuilder.RenameIndex(
                name: "IX_CourseTeacher_CourseId",
                table: "TeacherTeacher",
                newName: "IX_TeacherTeacher_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherTeacher",
                table: "TeacherTeacher",
                columns: new[] { "TeacherId", "CourseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherTeacher_Course_CourseId",
                table: "TeacherTeacher",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherTeacher_Teacher_TeacherId",
                table: "TeacherTeacher",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
