
using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Portal.Shared.Response;

public class SaveTeacherResponse
{
    public Guid TeacherId { get; set; } = Guid.NewGuid();
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Subject { get; set; }
    public string? Email { get; set; }
    public ICollection<CourseEntity>? Courses { get; set; }
}
