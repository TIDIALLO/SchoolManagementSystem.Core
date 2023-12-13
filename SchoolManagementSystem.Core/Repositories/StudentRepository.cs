using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Core.Api.Contracts;
using SchoolManagementSystem.DAL;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;

namespace SchoolManagementSystem.Core.Api.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public StudentRepository(IServiceProvider serviceProvider)
    {
        _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
    }

    public async Task<StudentEntity> SaveStudent(StudentEntity student)
    {
/*       var student = new StudentEntity
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth,
            Address = request.Address,
            Enrollments = request.Enrollments
        };*/
        await _dbContext.Students.AddAsync(student);
        await _dbContext.SaveChangesAsync();
        return student;
    }

    public Task<IActionResult> DeleteStudent(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<IEnumerable<StudentEntity>>> GetAllStudents()
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> GetStudentById(Guid id)
    {
        throw new NotImplementedException();
    }



    public Task<IActionResult> UpdateStudent(Guid id, StudentEntity student)
    {
        throw new NotImplementedException();
    }
}
