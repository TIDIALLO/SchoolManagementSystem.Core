using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Domain.Entities;

[Index(nameof(Email), IsUnique = true)]
[Table("students")]
public class StudentEntity
{
    [Key]
    [Column("id")]
    public Guid StudentId { get; set; } = Guid.NewGuid();
    [Column("firstname")]
    [StringLength(50)]
    public string FirstName { get; set; }
    [Column("lastname")]
    [StringLength(50)]
    public string LastName { get; set; }
    [Column("dateofbirth")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateOfBirth { get; set; }
    [Column("Email")]
    public string Email { get; set; }
    [Column("Address")]
    public string Address { get; set; }

    [Column("enrollments")]
    public ICollection<EnrollmentEntity> Enrollments { get; set; }
}
