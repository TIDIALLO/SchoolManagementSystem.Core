using SchoolManagementSystem.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolManagementSystem.Domain.Entities;

[Table("courses")]
public class CourseEntity: IEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
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
