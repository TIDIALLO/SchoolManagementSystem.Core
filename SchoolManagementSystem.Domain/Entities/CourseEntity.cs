using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolManagementSystem.Domain.Entities;

[Table("courses")]
public class CourseEntity
{
    [Key]
    [Column("courseid")]
    public Guid CourseId { get; set; }
    [Column("title")]
    public string? Title { get; set; }
    [Column("firstname")]
    public string? Description { get; set; }
    /*[Column("teacherid")]
            public Guid TeacherId { get; set; }

        [Column("teacher")]
        public TeacherEntity? Teacher { get; set; }*/

    /*      [Column("enrollments")]
        public ICollection<EnrollmentEntity>? Enrollments { get; set; }*/
}
