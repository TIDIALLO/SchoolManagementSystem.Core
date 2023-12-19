using SchoolManagementSystem.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace   SchoolManagementSystem.Domain.Entities;

[Table("enrollments")]
public class EnrollmentEntity: IEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("studentid")]
    public Guid StudentId { get; set; }
    [Column("courseid")]
    public Guid CourseId { get; set; }
    [Column("enrollmentdate")]
    public DateTime EnrollmentDate { get; set; }
    [Column("student")]
    public StudentEntity Student { get; set; }
    [Column("course")]
    public CourseEntity Course { get; set; }
}
