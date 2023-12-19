using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.DAL;

public class UnitOfWork(ApplicationDbContext context, IServiceProvider serviceProvider) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    public IGenericRepository<StudentEntity> Students { get; set; } = serviceProvider.GetRequiredService<IGenericRepository<StudentEntity>>();
    public IGenericRepository<CourseEntity> Courses { get; set; } = serviceProvider.GetRequiredService<IGenericRepository<CourseEntity>>();
   

    public void Commit()
    {
        _context.SaveChanges();
    }
}
