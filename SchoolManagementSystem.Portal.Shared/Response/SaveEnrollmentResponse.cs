
using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Portal.Shared.Response;

public class SaveEnrollmentResponse
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime EnrollmentDate { get; set; }

    public StudentEntity Student { get; set; }
    public CourseEntity Course { get; set; }
}
