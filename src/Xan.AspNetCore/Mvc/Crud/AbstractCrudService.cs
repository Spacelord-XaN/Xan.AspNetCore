using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Xan.AspNetCore.EntityFrameworkCore;
using Xan.AspNetCore.Models;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Mvc.Crud;

public abstract class AbstractCrudService<TEntity, TListParameter>
    : ICrudService<TEntity, TListParameter>
    where TEntity : class, ICrudEntity, new()
    where TListParameter : ListParameter
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

    public virtual async Task<TEntity> GetAsync(int id)
        => await Set.FirstByIdAsync(id);

    public virtual async Task<IPaginatedList<CrudItemModel<TEntity>>> GetManyAsync(TListParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);
        ArgumentNullException.ThrowIfNull(parameter.PageSize);

        IQueryable<TEntity> iq = OrderByDefault(Set);
        if (!string.IsNullOrEmpty(parameter.SearchString))
        {
            iq = iq.Where(Search(parameter.SearchString));
        }
        if (parameter.State.HasValue)
        {
            iq = iq.Where(basar => basar.State == parameter.State.Value);
        }
        
        IPaginatedList<CrudItemModel<TEntity>> items = await iq
            .AsPaginatedAsync(parameter.PageSize.Value, parameter.PageIndex, CreateItemModelAsync);
        return items;
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        Set.Update(entity);
        await _db.SaveChangesAsync();
    }

    protected virtual async Task<CrudItemModel<TEntity>> CreateItemModelAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        bool canDelete = await CanDeleteAsync(entity);
        CrudItemModel<TEntity> item = new(entity, canDelete);
        return item;
    }

    protected abstract IQueryable<TEntity> OrderByDefault(IQueryable<TEntity> iq);

    protected abstract Expression<Func<TEntity, bool>> Search(string searchString);
}
