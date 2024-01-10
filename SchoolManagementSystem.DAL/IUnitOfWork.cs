

using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.DAL;

public interface IUnitOfWork 
{
    Task Commit();
    
    IGenericRepository<StudentEntity> Students { get; set;}
    IGenericRepository<CourseEntity> Courses { get; set; }
    IGenericRepository<TeacherEntity> Teachers { get; set; }
    IGenericRepository<EnrollmentEntity> Enrollments { get; set; }
}
public interface IUnitOfWork<C>
{
    Task Commit();
}