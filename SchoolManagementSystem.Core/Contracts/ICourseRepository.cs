using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;

namespace SchoolManagementSystem.Core.Api.Contracts;

public interface ICourseRepository
{
    public Task<IActionResult> SaveCourse(SaveCourseRequest request);
    public Task<IActionResult> GetTeacherById(Guid id);
    public Task<ActionResult<IEnumerable<CourseEntity>>> GetAllCourses();
    public Task<IActionResult> UpdateCourse(Guid id, CourseEntity course);
    public Task<IActionResult> DeleteCourse(Guid id);
}
