using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Mvc.Crud;

public sealed class CrudListModel<TEntity, TListParameter>
    : PaginatedList<CrudItemModel<TEntity>>
    , ICrudListModel
    where TEntity : class, ICrudEntity, new()
    where TListParameter : ListParameter
{
    public delegate Task<IHtmlContent> CreateTableDelegate(ViewContext viewContext, IPaginatedList<CrudItemModel<TEntity>> items);

    private readonly CreateTableDelegate _createTableAsync;

    public CrudListModel(IPaginatedList<CrudItemModel<TEntity>> items, TListParameter parameter, CreateTableDelegate createTableAsync, ICrudRouter<TEntity> router, LocalizedString listTitle, LocalizedString createText)
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

    public ICrudRouter<TEntity> Router { get; }

    public LocalizedString ListTitle { get; }

    public LocalizedString CreateText { get; }

    public async Task<IHtmlContent> TableAsync(ViewContext viewContext)
    {
        ArgumentNullException.ThrowIfNull(viewContext);

        return await _createTableAsync(viewContext, this);
    }
}
