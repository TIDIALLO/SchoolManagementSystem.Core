using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Domain.Entities;


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
            public virtual DbSet<TeacherEntity> Teacher { get; set; }
            public virtual DbSet<CourseEntity> Courses { get; set; }
            public virtual DbSet<EnrollmentEntity> Enrollments { get; set; }
    }
}
