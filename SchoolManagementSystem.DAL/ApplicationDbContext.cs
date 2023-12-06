using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Domain.Models;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace SchoolManagementSystem.DAL
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
             
        }
        /*        public virtual DbSet<CourseModel> CourseModels { get; set; }
                public virtual DbSet<EnrollmentModel> EnrollmentModels { get; set; }
                public virtual DbSet<StudentModel> StudentModels { get; set; }
                public virtual DbSet<TeacherModel> TeacherModels { get; set; }*/
        public virtual DbSet<StudentEntity> Students { get; set; }
    }
}
