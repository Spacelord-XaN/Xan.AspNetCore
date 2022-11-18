using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.Parameter;

#nullable disable warnings
public class ListParameter
{
    public const int DefaultPageSize = 5;

    public ListParameter()
    { }

    public ListParameter(ListParameter other)
    {
        ArgumentNullException.ThrowIfNull(other);

        SearchString = other.SearchString;
        PageIndex = other.PageIndex;
        PageSize = other.PageSize;
        State = other.State;
    }

    public string SearchString { get; set; }

    public int PageIndex { get; set; } = 1;

    public int? PageSize { get; set; }

    public ObjectState? State { get; set; }

    public ListParameter ToPage(int pageIndex)
    {
        ListParameter parameter = Clone();
        parameter.PageIndex = pageIndex;
        return parameter;
    }

    public ListParameter ToPageSize(int? pageSize)
    {
        ListParameter parameter = Clone();
        parameter.PageSize = pageSize;
        return parameter;
    }

    protected virtual ListParameter Clone()
        => new(this);
}
