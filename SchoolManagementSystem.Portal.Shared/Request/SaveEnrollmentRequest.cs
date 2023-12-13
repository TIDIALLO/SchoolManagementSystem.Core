
using SchoolManagementSystem.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Portal.Shared.Request;

public class SaveEnrollmentRequest
{
    //public Guid EnrollmentId { get; set; } = Guid.NewGuid();
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime EnrollmentDate { get; set; }

    public StudentEntity? Student { get; set; }
    public CourseEntity? Course { get; set; }
}
