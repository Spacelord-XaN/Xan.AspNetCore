using Microsoft.EntityFrameworkCore;
using Xan.Extensions.Collections;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.EntityFrameworkCore;

public static class PaginationExtensions
{
    public static async Task<IPaginatedList<T>> AsPaginatedAsync<T>(this IQueryable<T> iq, int pageSize, int pageIndex)
    {
        ArgumentNullException.ThrowIfNull(iq);

        return await iq.AsPaginatedAsync(pageSize, pageIndex, x => x);
    }

    public static async Task<IPaginatedList<TResult>> AsPaginatedAsync<TSource, TResult>(this IQueryable<TSource> iq, int pageSize, int pageIndex, Func<TSource, TResult> selector)
    {
        ArgumentNullException.ThrowIfNull(iq);
        ArgumentNullException.ThrowIfNull(selector);

        return await iq.AsPaginatedAsync(pageSize, pageIndex, async source => await Task.FromResult(selector(source)));
    }

    public static async Task<IPaginatedList<TResult>> AsPaginatedAsync<TSource, TResult>(this IQueryable<TSource> iq, int pageSize, int pageIndex, Func<TSource, Task<TResult>> selectorAsync)
    {
        ArgumentNullException.ThrowIfNull(iq);
        ArgumentNullException.ThrowIfNull(selectorAsync);

        if (pageIndex == 0)
        {
            pageIndex = 1;
        }

        int totalItemCount = await iq.CountAsync();
        int totalPageCount = IPaginated.CalcTotalPages(totalItemCount, pageSize);
        pageIndex = Math.Min(pageIndex, Math.Max(1, totalPageCount));

        IQueryable<TSource> paginatedIq = iq;
        if (pageSize != IPaginated.AllPageSize)
        {
            int skipCount = Math.Max(pageIndex - 1, 0) * pageSize;
            paginatedIq = iq.Skip(skipCount).Take(pageSize);
        }
        IReadOnlyList<TSource> sourceItems = await paginatedIq.ToArrayAsync();
        List<TResult> resultItems = new();
        foreach (TSource sourceItem in sourceItems)
        {
            resultItems.Add(await selectorAsync(sourceItem));
        }
        return new PaginatedList<TResult>(resultItems, pageIndex, pageSize, totalPageCount, totalItemCount);
    }
}
