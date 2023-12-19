
namespace SchoolManagementSystem.DAL;

public interface IGenericRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity> GetByIdAsync(Guid id);
    Task AddAsync(TEntity entity);
    Task<IQueryable<TEntity>> GetAll();
    Task UpdateAsync(TEntity entity);
    Task<bool> RemoveAsync(TEntity entity);

}

