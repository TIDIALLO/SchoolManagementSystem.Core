
using SchoolManagementSystem.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Portal.Shared.Request;

public class SaveTeacherRequest
{
   // public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Subject { get; set; }
    public string? Email { get; set; }
    public ICollection<CourseEntity>? Courses { get; set; }
}
