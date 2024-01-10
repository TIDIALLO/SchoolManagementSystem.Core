using SchoolManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Portal.Shared.Response
{
    public class SaveStudentResponse
    {
        public Guid StudentId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        //public ICollection<EnrollmentEntity> Enrollments { get; set; }

    }
}
