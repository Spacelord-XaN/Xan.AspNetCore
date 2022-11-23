using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Xan.AspNetCore.EntityFrameworkCore;
using Xan.AspNetCore.Models;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.Extensions.Collections.Generic;

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

    public abstract IQueryable<TEntity> DefaultOrder(IQueryable<TEntity> set);

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

    public IQueryable<TEntity> GetMany(string? searchString = null, ObjectState? state = null)
    {
        IQueryable<TEntity> iq = Set;
        if (!string.IsNullOrEmpty(searchString))
        {
            iq = iq.Where(Search(searchString));
        }
        if (state.HasValue)
        {
            iq = iq.Where(entity => entity.State == state.Value);
        }
        return DefaultOrder(iq);
    }

    public async Task<IPaginatedList<CrudItemModel<TEntity>>> GetManyAsync(int pageSize, int pageIndex, string? searchString = null, ObjectState? state = null)
        => await GetMany(searchString, state)
            .AsPaginatedAsync(pageSize, pageIndex, async entity =>
            {
                bool canDelete = await CanDeleteAsync(entity);
                CrudItemModel<TEntity> item = new(entity, canDelete);
                return item;
            });

    public abstract Expression<Func<TEntity, bool>> Search(string searchString);
}
