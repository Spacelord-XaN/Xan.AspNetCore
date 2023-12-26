using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Mvc.Crud;

public abstract class AbstractCrudModelFactory<TEntity, TListParameter, TRouter>
    : ICrudModelFactory<TEntity, TListParameter>
    where TEntity : class, ICrudEntity, new()
    where TListParameter : ListParameter, new()
    where TRouter : ICrudRouter

{
    public AbstractCrudModelFactory(TRouter router)
    {
        Router = router ?? throw new ArgumentNullException(nameof(router));
    }

    protected TRouter Router { get; }

    protected abstract string CreateTitle { get; }

    protected abstract string EditTitle { get; }

    protected abstract string ListTitle { get; }

    public async Task<ICrudModel> CreateModelAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        ICrudModel model = new CrudModel<TEntity>(entity, CreateEditorAsync, CreateTitle);
        return await Task.FromResult(model);
    }

    public async Task<ICrudModel> EditModelAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        ICrudModel model = new CrudModel<TEntity>(entity, CreateEditorAsync, EditTitle);
        return await Task.FromResult(model);
    }

    public async Task<ICrudListModel> ListModelAsync(IPaginatedList<CrudItemModel<TEntity>> items, TListParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(items);

        ICrudListModel model = new CrudListModel<TEntity, TListParameter, TRouter>(items, parameter, CreateTableAsync, Router, ListTitle, CreateTitle);
        return await Task.FromResult(model);
    }

    protected abstract Task<IHtmlContent> CreateEditorAsync(ViewContext viewContext, TEntity entity);

    protected abstract Task<IHtmlContent> CreateTableAsync(ViewContext viewContext, IPaginatedList<CrudItemModel<TEntity>> model);
}
