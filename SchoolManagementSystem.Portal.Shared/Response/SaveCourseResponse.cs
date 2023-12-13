
using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Portal.Shared.Response;

public class SaveCourseResponse
{
    public Guid CourseId { get; set; } = Guid.NewGuid();
    public string? Title { get; set; }
    public string? Description { get; set; }
/*    public Guid TeacherId { get; set; }

    public TeacherEntity Teacher { get; set; }
    public ICollection<EnrollmentEntity> Enrollments { get; set; }*/
}
