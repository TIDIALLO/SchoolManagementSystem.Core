using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Portal.Shared.Request;

namespace SchoolManagementSystem.Core.Api.Contracts;

public interface IEnrollmentRepository
{
    public Task<IActionResult> SaveEnrollment(SaveEnrollmentRequest request);
    public Task<IActionResult> GetTeacherById(Guid id);
    public Task<ActionResult<IEnumerable<EnrollmentEntity>>> GetAllEnrollments();
    public Task<IActionResult> UpdateEnrollment(Guid id, EnrollmentEntity enrollment);
    public Task<IActionResult> DeleteEnrollment(Guid id);

}
