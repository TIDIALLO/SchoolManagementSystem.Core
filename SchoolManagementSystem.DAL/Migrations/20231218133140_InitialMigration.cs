using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_teachers_TeacherEntityTeacherId",
                table: "courses");

            migrationBuilder.RenameColumn(
                name: "teacherid",
                table: "teachers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "enrollmentid",
                table: "enrollments",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TeacherEntityTeacherId",
                table: "courses",
                newName: "TeacherEntityId");

            migrationBuilder.RenameColumn(
                name: "courseid",
                table: "courses",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_courses_TeacherEntityTeacherId",
                table: "courses",
                newName: "IX_courses_TeacherEntityId");

            migrationBuilder.AlterColumn<string>(
                name: "subject",
                table: "teachers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "lastname",
                table: "teachers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "firstname",
                table: "teachers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "teachers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "lastname",
                table: "students",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "firstname",
                table: "students",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "students",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "students",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_courses_teachers_TeacherEntityId",
                table: "courses",
                column: "TeacherEntityId",
                principalTable: "teachers",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_teachers_TeacherEntityId",
                table: "courses");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "teachers",
                newName: "teacherid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "enrollments",
                newName: "enrollmentid");

            migrationBuilder.RenameColumn(
                name: "TeacherEntityId",
                table: "courses",
                newName: "TeacherEntityTeacherId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "courses",
                newName: "courseid");

            migrationBuilder.RenameIndex(
                name: "IX_courses_TeacherEntityId",
                table: "courses",
                newName: "IX_courses_TeacherEntityTeacherId");

            migrationBuilder.AlterColumn<string>(
                name: "subject",
                table: "teachers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "lastname",
                table: "teachers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "firstname",
                table: "teachers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "teachers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "lastname",
                table: "students",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "firstname",
                table: "students",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "students",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "students",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_teachers_TeacherEntityTeacherId",
                table: "courses",
                column: "TeacherEntityTeacherId",
                principalTable: "teachers",
                principalColumn: "teacherid");
        }
    }
}
