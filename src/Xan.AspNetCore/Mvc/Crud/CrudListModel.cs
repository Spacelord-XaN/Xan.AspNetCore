using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Mvc.Crud;

public sealed class CrudListModel<TEntity, TListParameter, TRouter>
    : PaginatedList<CrudItemModel<TEntity>>
    , ICrudListModel
    where TEntity : class, ICrudEntity, new()
    where TListParameter : ListParameter
    where TRouter : ICrudRouter
{
    public delegate Task<IHtmlContent> CreateTableDelegate(ViewContext viewContext, IPaginatedList<CrudItemModel<TEntity>> items);

    private readonly CreateTableDelegate _createTableAsync;

    public CrudListModel(IPaginatedList<CrudItemModel<TEntity>> items, TListParameter parameter, CreateTableDelegate createTableAsync, TRouter router, string listTitle, string createText)
        : base(items)
    {
        _createTableAsync = createTableAsync;
        Parameter = parameter;
        Router = router;
        ListTitle = listTitle;
        CreateText = createText;
    }

    ListParameter ICrudListModel.Parameter { get => Parameter; }

    public TListParameter Parameter { get; }

    ICrudRouter ICrudListModel.Router { get => Router; }

    public TRouter Router { get; }

    public string ListTitle { get; }

    public string CreateText { get; }

    public async Task<IHtmlContent> TableAsync(ViewContext viewContext)
    {
        ArgumentNullException.ThrowIfNull(viewContext);

        return await _createTableAsync(viewContext, this);
    }
}
