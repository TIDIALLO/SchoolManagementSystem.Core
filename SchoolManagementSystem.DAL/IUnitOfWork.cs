

using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.DAL;

public interface IUnitOfWork 
{
    void Commit();
    
    IGenericRepository<StudentEntity> Students { get; set;}
    IGenericRepository<CourseEntity> Courses { get; set; }
}
public interface IUnitOfWork<C>
{
    void Commit();
}