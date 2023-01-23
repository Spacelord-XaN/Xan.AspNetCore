using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Models;

public sealed class ListModel<T, TParameter>
    : PaginatedList<T>
    where TParameter : ListParameter
{
    public ListModel(IPaginatedList<T> items, TParameter parameter)
        : base(items)
    {
        Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
    }

    public TParameter Parameter { get; }
}
