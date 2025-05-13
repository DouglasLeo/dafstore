using System.Linq.Expressions;
using dafstore.Application.Shared.Abstractions.Repositories;
using dafstore.Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace dafstore.Infrastructure.Persistence.Shared.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private bool _disposed;

    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }

    public async Task<List<TEntity>> FindAllAsync() => await DbSet.AsNoTracking().ToListAsync();

    public async Task<TEntity?> FindByIdAsync(Guid id) => await DbSet.FindAsync(id);

    public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate) =>
        await DbSet.AsNoTracking().Where(predicate).ToListAsync();

    public async Task AddAsync(TEntity entity) => await DbSet.AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await DbSet.AddRangeAsync(entities);

    public async Task UpdateAsync(TEntity entity) => await Task.Run(() => DbSet.Update(entity));

    public async Task RemoveAsync(TEntity entity) => await Task.Run(() => DbSet.Remove(entity));

    public async Task<int> SaveChangesAsync() => await Context.SaveChangesAsync();

    public void Dispose()
    {
        if (_disposed) return;

        Context.Dispose();
        GC.SuppressFinalize(this);
        _disposed = true;
    }
}