using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.EntityFrameworkCore;

public static class ICrudEntityExtensions
{
    public static IQueryable<TEntity> WhereDisabled<TEntity>(this IQueryable<TEntity> iq, int id)
        where TEntity : ICrudEntity
    {
        ArgumentNullException.ThrowIfNull(iq);

        return iq.Where(enttiy => enttiy.State == ObjectState.Disabled);
    }

    public static IQueryable<TEntity> WhereEnabled<TEntity>(this IQueryable<TEntity> iq, int id)
        where TEntity : ICrudEntity
    {
        ArgumentNullException.ThrowIfNull(iq);

        return iq.Where(enttiy => enttiy.State == ObjectState.Enabled);
    }
}
