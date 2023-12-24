using Microsoft.EntityFrameworkCore;
using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.Mvc.Crud;

public interface ICrudService<TEntity>
    where TEntity : class, ICrudEntity, new()
{
    DbSet<TEntity> Set { get; }

    Task<bool> CanDeleteAsync(TEntity entity);

    Task<TEntity> CreateNewAsync();

    Task<int> CreateAsync(TEntity entity);

    Task DeleteAsync(int id);

    Task<TEntity> GetAsync(int id);

    Task UpdateAsync(TEntity entity);

    Task DisableAsync(int id);

    Task EnableAsync(int id);
}
