using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Domain.Entities;


namespace SchoolManagementSystem.DAL
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
             
        }
        public virtual DbSet<StudentEntity> Students { get; set; }  
        public virtual DbSet<TeacherEntity> Teacher { get; set; }
        public virtual DbSet<CourseEntity> Courses { get; set; }
        public virtual DbSet<EnrollmentEntity> Enrollments { get; set;}
    }
}
