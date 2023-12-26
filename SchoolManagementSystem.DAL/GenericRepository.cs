using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.DAL;

public class GenericRepository<TEntity>(ApplicationDbContext dbContext) : IGenericRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        await Task.CompletedTask;
        return _dbContext.Set<TEntity>().FirstOrDefault(x => x.Id == id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
    }

    public async Task<bool> RemoveAsync(TEntity entity)
    {
        await Task.CompletedTask;
        if (_dbContext.Entry(entity).State == EntityState.Detached) _dbContext.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Deleted;
        _dbContext.Set<TEntity>().Remove(entity);
        return true;
    }

    public async Task<IQueryable<TEntity>> GetAll()
    {
        await Task.CompletedTask;
        return _dbContext.Set<TEntity>().AsNoTracking();
    }


    public async Task UpdateAsync(TEntity entity)
    {
        // await Task.CompletedTask;
        var st = await _dbContext.Set<TEntity>().FindAsync(entity.Id);
        if (st != null)
        {
            _dbContext.Entry(st).State = EntityState.Modified;
        }
        _dbContext.Set<TEntity>().Update(entity);
    }   
}
