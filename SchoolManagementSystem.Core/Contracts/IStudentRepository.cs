using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;

namespace SchoolManagementSystem.Core.Api.Contracts;

public interface IStudentRepository
{
    //public  Task<IActionResult> SaveStudent(SaveStudentRequest request);
    public Task<StudentEntity> SaveStudent(StudentEntity student);
    public  Task<IActionResult> GetStudentById(Guid id);
    public  Task<ActionResult<IEnumerable<StudentEntity>>> GetAllStudents();
    public Task<IActionResult> UpdateStudent(Guid id, StudentEntity student);
    public  Task<IActionResult> DeleteStudent(Guid id);
}
