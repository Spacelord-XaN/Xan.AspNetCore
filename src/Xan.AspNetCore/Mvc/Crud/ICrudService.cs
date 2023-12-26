using Microsoft.EntityFrameworkCore;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Mvc.Crud;

public interface ICrudService<TEntity, TListParameter>
    where TEntity : class, ICrudEntity, new()
    where TListParameter : ListParameter
{
    DbSet<TEntity> Set { get; }

    Task<bool> CanDeleteAsync(TEntity entity);

    Task<TEntity> CreateNewAsync();

    Task<int> CreateAsync(TEntity entity);

    Task DeleteAsync(int id);

    Task DisableAsync(int id);

    Task EnableAsync(int id);

    Task<TEntity> GetAsync(int id);

    Task<IPaginatedList<CrudItemModel<TEntity>>> GetManyAsync(TListParameter parameter);

    Task UpdateAsync(TEntity entity);
}
