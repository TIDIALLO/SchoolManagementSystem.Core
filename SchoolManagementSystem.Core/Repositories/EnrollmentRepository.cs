using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Core.Api.Contracts;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;

namespace SchoolManagementSystem.Core.Api.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    public Task<IActionResult> DeleteEnrollment(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<IEnumerable<EnrollmentEntity>>> GetAllEnrollments()
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> GetTeacherById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> SaveEnrollment(SaveEnrollmentRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> UpdateEnrollment(Guid id, EnrollmentEntity enrollment)
    {
        throw new NotImplementedException();
    }
}
