using System.Linq.Expressions;

namespace dafstore.Application.Shared.Abstractions.Repositories;

public interface IRepository<TEntity>
{
    Task<List<TEntity>> FindAllAsync();
    Task<TEntity?> FindByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task<int> SaveChangesAsync();
}