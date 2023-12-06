using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolManagementSystem.Domain.Entities;

[Table("enrollments")]
public class EnrollmentEntity
{
    [Key]
    [Column("enrollmentid")]
    public Guid EnrollmentId { get; set; }
    [Column("studentid")]
    public Guid StudentId { get; set; }
    [Column("courseid")]
    public Guid CourseId { get; set; }
    [Column("enrollmentdate")]
    public DateTime EnrollmentDate { get; set; }

    public StudentEntity Student { get; set; }
    public CourseEntity Course { get; set; }
}
