using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teacher_teacherid",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_courseid",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_studentid",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teacher",
                table: "Teacher");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "students");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "enrollments");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "courses");

            migrationBuilder.RenameTable(
                name: "Teacher",
                newName: "teachers");

            migrationBuilder.RenameIndex(
                name: "IX_Students_Email",
                table: "students",
                newName: "IX_students_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_studentid",
                table: "enrollments",
                newName: "IX_enrollments_studentid");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_courseid",
                table: "enrollments",
                newName: "IX_enrollments_courseid");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_teacherid",
                table: "courses",
                newName: "IX_courses_teacherid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_students",
                table: "students",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_enrollments",
                table: "enrollments",
                column: "enrollmentid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_courses",
                table: "courses",
                column: "courseid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_teachers",
                table: "teachers",
                column: "teacherid");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_teachers_teacherid",
                table: "courses",
                column: "teacherid",
                principalTable: "teachers",
                principalColumn: "teacherid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_enrollments_courses_courseid",
                table: "enrollments",
                column: "courseid",
                principalTable: "courses",
                principalColumn: "courseid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_enrollments_students_studentid",
                table: "enrollments",
                column: "studentid",
                principalTable: "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_teachers_teacherid",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_courses_courseid",
                table: "enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_students_studentid",
                table: "enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_students",
                table: "students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_enrollments",
                table: "enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_courses",
                table: "courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_teachers",
                table: "teachers");

            migrationBuilder.RenameTable(
                name: "students",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "enrollments",
                newName: "Enrollments");

            migrationBuilder.RenameTable(
                name: "courses",
                newName: "Courses");

            migrationBuilder.RenameTable(
                name: "teachers",
                newName: "Teacher");

            migrationBuilder.RenameIndex(
                name: "IX_students_Email",
                table: "Students",
                newName: "IX_Students_Email");

            migrationBuilder.RenameIndex(
                name: "IX_enrollments_studentid",
                table: "Enrollments",
                newName: "IX_Enrollments_studentid");

            migrationBuilder.RenameIndex(
                name: "IX_enrollments_courseid",
                table: "Enrollments",
                newName: "IX_Enrollments_courseid");

            migrationBuilder.RenameIndex(
                name: "IX_courses_teacherid",
                table: "Courses",
                newName: "IX_Courses_teacherid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                column: "enrollmentid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "courseid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teacher",
                table: "Teacher",
                column: "teacherid");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teacher_teacherid",
                table: "Courses",
                column: "teacherid",
                principalTable: "Teacher",
                principalColumn: "teacherid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_courseid",
                table: "Enrollments",
                column: "courseid",
                principalTable: "Courses",
                principalColumn: "courseid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_studentid",
                table: "Enrollments",
                column: "studentid",
                principalTable: "Students",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
