﻿using SchoolManagementSystem.Domain.Entities;
namespace SchoolManagementSystem.Portal.Shared.Request
{
    public class SaveStudentRequest
    {
        //public Guid StudentId { get; set; } 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        
        public DateTimeOffset DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public ICollection<EnrollmentEntity>? Enrollments { get; set; }

    }
}
