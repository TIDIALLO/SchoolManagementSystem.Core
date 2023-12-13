using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Core.Api.Contracts;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;

namespace SchoolManagementSystem.Core.Api.Repositories;

public class TeacherRepository : ITeacherRepository
{
    public Task<IActionResult> DeletTeacher(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<IEnumerable<TeacherEntity>>> GetAllTeachers()
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> GetTacherById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> SaveTeacher(SaveTeacherRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> UpdateTeacher(Guid id, TeacherEntity teacher)
    {
        throw new NotImplementedException();
    }
}
