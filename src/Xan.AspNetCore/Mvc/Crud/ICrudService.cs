using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Mvc.Crud;

public interface ICrudService<TEntity>
    where TEntity : class, ICrudEntity, new()
{
    DbSet<TEntity> Set { get; }

    Task<bool> CanDeleteAsync(TEntity entity);

    Task<TEntity> CreateNewAsync();

    Task<int> CreateAsync(TEntity entity);

    IQueryable<TEntity> DefaultOrder(IQueryable<TEntity> set);

    Task DeleteAsync(int id);

    Task<TEntity> GetAsync(int id);

    Task UpdateAsync(TEntity entity);

    Task DisableAsync(int id);

    Task EnableAsync(int id);

    Task<IPaginatedList<CrudItemModel<TEntity>>> GetManyAsync(int pageSize, int pageIndex, string? searchString = null, ObjectState? state = null);

    Expression<Func<TEntity, bool>> Search(string searchString);
}
