using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Mvc.Crud;

public sealed class CrudListModel<TEntity, TListParameter, TRouter>(IPaginatedList<CrudItemModel<TEntity>> items, TListParameter parameter, CrudListModel<TEntity, TListParameter, TRouter>.CreateTableDelegate createTableAsync, TRouter router, string listTitle, string createText)
    : PaginatedList<CrudItemModel<TEntity>>(items)
    , ICrudListModel
    where TEntity : class, ICrudEntity, new()
    where TListParameter : ListParameter
    where TRouter : ICrudRouter
{
    public delegate Task<IHtmlContent> CreateTableDelegate(ViewContext viewContext, IPaginatedList<CrudItemModel<TEntity>> items);

    ListParameter ICrudListModel.Parameter { get => Parameter; }

    public TListParameter Parameter { get; } = parameter ?? throw new ArgumentNullException(nameof(parameter));

    ICrudRouter ICrudListModel.Router { get => Router; }

    public TRouter Router { get; } = router ?? throw new ArgumentNullException(nameof(router));

    public string ListTitle { get; } = listTitle ?? throw new ArgumentNullException(nameof(listTitle));

    public string CreateText { get; } = createText ?? throw new ArgumentNullException(nameof(createText));

    public async Task<IHtmlContent> TableAsync(ViewContext viewContext)
    {
        ArgumentNullException.ThrowIfNull(viewContext);

        return await createTableAsync(viewContext, this);
    }
}
