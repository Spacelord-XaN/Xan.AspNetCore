using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections;

namespace Xan.AspNetCore.Models;

public sealed class PaginationModel(IPaginatedList items, ListParameter currentParameter, Func<ListParameter, string> getListUrl)
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

        if (pageIndex <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageIndex), "The page index must be greater that 0. The first page has index 1.");
        }
        if (pageIndex > totalPages)
        {
            throw new ArgumentOutOfRangeException(nameof(pageIndex), "The page index is greater than the total pages.");
        }
        if (totalPages <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalPages), "Total pages must be greater that 0.");
        }

        if (totalPages <=0)
        {
            yield break;
        }

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

    public IPaginatedList Items { get; } = items ?? throw new ArgumentNullException(nameof(items));

    public IEnumerable<int?> GetPages()
        => GetPages(Items.PageIndex, Items.TotalPageCount);

    public bool IsActive(int page)
        => page == Items.PageIndex;

    public string ToPageSize(int? pageSize)
        => GetListUrl(currentParameter.ToPageSize(pageSize));

    public string ToNextPage()
        => ToPage(Items.PageIndex + 1);

    public string ToPage(int page)
        => GetListUrl(currentParameter.ToPage(page));
    
    public string ToPreviousPage()
        => ToPage(Items.PageIndex - 1);

    private string GetListUrl(ListParameter parameter)
        => getListUrl(parameter);
}
