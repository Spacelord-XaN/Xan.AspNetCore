using Microsoft.EntityFrameworkCore;
using Xan.AspNetCore.EntityFrameworkCore;
using Xan.AspNetCore.Models;
using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.Mvc.Crud;

public abstract class AbstractCrudService<TEntity>
    : ICrudService<TEntity>
    where TEntity : class, ICrudEntity, new()
{
    private readonly DbContext _db;

    public AbstractCrudService(DbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public abstract DbSet<TEntity> Set { get; }

    public abstract Task<bool> CanDeleteAsync(TEntity entity);

    public virtual async Task<TEntity> CreateNewAsync()
        => await Task.FromResult(new TEntity());

    public virtual async Task<int> CreateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        Set.Add(entity);
        await _db.SaveChangesAsync();

        return entity.Id;
    }

    public virtual async Task DeleteAsync(int id)
    {
        TEntity entity = await Set.FirstByIdAsync(id);
        Set.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public virtual async Task<TEntity> GetAsync(int id)
        => await Set.FirstByIdAsync(id);

    public virtual async Task UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        Set.Update(entity);
        await _db.SaveChangesAsync();
    }

    public virtual async Task DisableAsync(int id)
    {
        TEntity entity = await Set.FirstByIdAsync(id);
        entity.State = ObjectState.Disabled;
        await _db.SaveChangesAsync();
    }

    public virtual async Task EnableAsync(int id)
    {
        TEntity entity = await Set.FirstByIdAsync(id);
        entity.State = ObjectState.Enabled;
        await _db.SaveChangesAsync();
    }
}
