
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolManagementSystem.Domain.Entities;

[Table("teachers")]
[Index(nameof(Email), IsUnique = true)]
public class TeacherEntity:IEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("firstname")]
    public string? FirstName { get; set; }
    [Column("lastname")]
    public string? LastName { get; set; }
    [Column("subject")]
    public string? Subject { get; set; }
    [Column("email")]
    public string? Email { get; set; }

    /*[Column("courses")]
    public ICollection<CourseEntity>? Courses { get; set; }*/
}
