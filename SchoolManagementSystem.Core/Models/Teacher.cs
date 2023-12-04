using SchoolManagementSystem.Core.Controllers;

namespace SchoolManagementSystem.Core.Models
{
    public class Teacher
    {

        public Guid TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
