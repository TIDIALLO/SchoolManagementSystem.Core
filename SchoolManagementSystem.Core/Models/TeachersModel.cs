using SchoolManagementSystem.Core.Controllers;

namespace SchoolManagementSystem.Core.Models
{
    public class TeachersModel
    {

        public Guid TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
