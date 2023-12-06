using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenamingColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teacher_TeacherId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Student_StudentId",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Teacher",
                newName: "subject");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Teacher",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Teacher",
                newName: "firstname");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Teacher",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Teacher",
                newName: "teacherid");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Enrollments",
                newName: "studentid");

            migrationBuilder.RenameColumn(
                name: "EnrollmentDate",
                table: "Enrollments",
                newName: "enrollmentdate");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Enrollments",
                newName: "courseid");

            migrationBuilder.RenameColumn(
                name: "EnrollmentId",
                table: "Enrollments",
                newName: "enrollmentid");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                newName: "IX_Enrollments_studentid");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                newName: "IX_Enrollments_courseid");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Courses",
                newName: "teacherid");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Courses",
                newName: "courseid");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Courses",
                newName: "firstname");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                newName: "IX_Courses_teacherid");

            migrationBuilder.RenameIndex(
                name: "IX_Student_Email",
                table: "Students",
                newName: "IX_Students_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameColumn(
                name: "subject",
                table: "Teacher",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "Teacher",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "Teacher",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Teacher",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "teacherid",
                table: "Teacher",
                newName: "TeacherId");

            migrationBuilder.RenameColumn(
                name: "studentid",
                table: "Enrollments",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "enrollmentdate",
                table: "Enrollments",
                newName: "EnrollmentDate");

            migrationBuilder.RenameColumn(
                name: "courseid",
                table: "Enrollments",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "enrollmentid",
                table: "Enrollments",
                newName: "EnrollmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_studentid",
                table: "Enrollments",
                newName: "IX_Enrollments_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_courseid",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseId");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Courses",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "teacherid",
                table: "Courses",
                newName: "TeacherId");

            migrationBuilder.RenameColumn(
                name: "courseid",
                table: "Courses",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "Courses",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_teacherid",
                table: "Courses",
                newName: "IX_Courses_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_Email",
                table: "Student",
                newName: "IX_Student_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teacher_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Student_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
