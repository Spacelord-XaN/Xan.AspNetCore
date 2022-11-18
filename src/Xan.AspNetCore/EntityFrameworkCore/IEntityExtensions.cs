﻿using Microsoft.EntityFrameworkCore;
using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.EntityFrameworkCore;

public static class IEntityExtensions
{
    public static async Task<TEntity> FirstByIdAsync<TEntity>(this IQueryable<TEntity> iq, int id)
        where TEntity : IEntity
    {
        ArgumentNullException.ThrowIfNull(iq);

        return await iq.FirstAsync(entity => entity.Id == id);
    }

    public static async Task<TEntity?> FirstOrDefaultByIdAsync<TEntity>(this IQueryable<TEntity> iq, int id)
        where TEntity : IEntity
    {
        ArgumentNullException.ThrowIfNull(iq);

        return await iq.FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public static IQueryable<TEntity> OrderById<TEntity>(this IQueryable<TEntity> iq)
        where TEntity : IEntity
    {
        ArgumentNullException.ThrowIfNull(iq);

        return iq.OrderBy(entity => entity.Id);
    }
}
