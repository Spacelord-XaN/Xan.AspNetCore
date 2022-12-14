using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections;

namespace Xan.AspNetCore.Models;

public sealed class PaginationModel
{
    public static IEnumerable<int?> GetPages(int pageIndex, int totalPages)
    {
        int first = 1;
        int last = totalPages;
        int twoBefore = pageIndex - 2;
        int before = pageIndex - 1;
        int after = pageIndex + 1;
        int twoAfter = pageIndex + 2;
        int gapToFirst = twoBefore - first;
        int gapToLast = totalPages - twoAfter;

        if (totalPages > 0)
        {
            yield return first;
        }

        if (gapToFirst > 2)
        {
            yield return null;
        }
        else if (gapToFirst > 1)
        {
            yield return first + 1;
        }

        if (twoBefore >= 1 && twoBefore != first)
        {
            yield return twoBefore;
        }

        if (before >= 1 && before != first)
        {
            yield return before;
        }

        if (pageIndex != first && pageIndex != last)
        {
            yield return pageIndex;
        }

        if (after <= totalPages && after != last)
        {
            yield return after;
        }

        if (twoAfter <= totalPages && twoAfter != last)
        {
            yield return twoAfter;
        }

        if (gapToLast > 2)
        {
            yield return null;
        }
        else if (gapToLast > 1)
        {
            yield return last - 1;
        }

        if (totalPages > 0 && last != first)
        {
            yield return last;
        }
    }

    public static IEnumerable<int> GetPageSizes()
    {
        yield return 5;
        yield return 10;
        yield return IPaginatedList.AllPageSize;
    }

    private readonly Func<ListParameter, string> _toList;
    private readonly ListParameter _currentParameter;

    public PaginationModel(IPaginatedList items, ListParameter currentParameter, Func<ListParameter, string> toList)
    {
        Items = items ?? throw new ArgumentNullException(nameof(items));
        _toList = toList ?? throw new ArgumentNullException(nameof(toList));
        _currentParameter = currentParameter ?? throw new ArgumentNullException(nameof(currentParameter));
    }

    public IPaginatedList Items { get; }

    public IEnumerable<int?> GetPages()
        => GetPages(Items.PageIndex, Items.TotalPageCount);

    public bool IsActive(int page)
        => page == Items.PageIndex;

    public string ToPageSize(int? pageSize)
        => ToList(_currentParameter.ToPageSize(pageSize));

    public string ToNextPage()
        => ToPage(Items.PageIndex + 1);

    public string ToPage(int page)
        => ToList(_currentParameter.ToPage(page));
    
    public string ToPreviousPage()
        => ToPage(Items.PageIndex - 1);

    private string ToList(ListParameter parameter)
        => _toList(parameter);
}
