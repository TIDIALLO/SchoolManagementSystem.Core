using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;

namespace SchoolManagementSystem.Core.Api.Contracts;

public interface ITeacherRepository
{
    public Task<IActionResult> SaveTeacher(SaveTeacherRequest request);
    public Task<IActionResult> GetTacherById(Guid id);
    public Task<ActionResult<IEnumerable<TeacherEntity>>> GetAllTeachers();
    public Task<IActionResult> UpdateTeacher(Guid id, TeacherEntity teacher);
    public Task<IActionResult> DeletTeacher(Guid id);
}
