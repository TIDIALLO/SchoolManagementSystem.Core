
using SchoolManagementSystem.Domain.Entities;
namespace SchoolManagementSystem.Portal.Shared.Request;

public class SaveCourseRequest
{
    public Guid CourseId { get; set; }  = Guid.NewGuid();
    public string? Title { get; set; }
    public string? Description { get; set; }
    /*ublic Guid TeacherId { get; set; }

    public TeacherEntity? Teacher { get; set; }
    public ICollection<EnrollmentEntity>? Enrollments { get; set; }*/
}
