using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class StudentEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_teachers_TeacherEntityId",
                table: "courses");

            migrationBuilder.DropIndex(
                name: "IX_courses_TeacherEntityId",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "TeacherEntityId",
                table: "courses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeacherEntityId",
                table: "courses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_courses_TeacherEntityId",
                table: "courses",
                column: "TeacherEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_teachers_TeacherEntityId",
                table: "courses",
                column: "TeacherEntityId",
                principalTable: "teachers",
                principalColumn: "id");
        }
    }
}
