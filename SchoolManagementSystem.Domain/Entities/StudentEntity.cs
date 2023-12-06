using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Domain.Entities;


[Table("Student")]
[Index(nameof(Email), IsUnique = true)]
[Keyless]
public class StudentEntity
{
    [Column("id")]
    public Guid StudentId { get; set; } = Guid.NewGuid();
    [Column("firstname")]
    public string FirstName { get; set; }
    [Column("lastname")]
    public string LastName { get; set; }
    [Column("dateofbirth")]
    public DateTime DateOfBirth { get; set; }
    [Column("Email")]
    public string Email { get; set; }
    [Column("Address")]
    public string Address { get; set; }
}
