﻿using Microsoft.AspNetCore.Builder;

namespace SchoolManagementSystem.Core.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
