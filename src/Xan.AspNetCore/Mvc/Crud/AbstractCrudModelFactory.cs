using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.Mvc.Crud;

public abstract class AbstractCrudModelFactory<TEntity>
    : ICrudModelFactory<TEntity, ListParameter>
    where TEntity : class, ICrudEntity, new()
{
    public AbstractCrudModelFactory(ICrudRouter<TEntity> router)
    {
        Router = router ?? throw new ArgumentNullException(nameof(router));
    }

    protected ICrudRouter<TEntity> Router { get; }

    protected abstract LocalizedString CreateTitle { get; }

    protected abstract LocalizedString EditTitle { get; }

    protected abstract LocalizedString ListTitle { get; }

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

    public async Task<ICrudListModel> ListModelAsync(IPaginatedList<CrudItemModel<TEntity>> items, ListParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(items);

        ICrudListModel model = new CrudListModel<TEntity, ListParameter>(items, parameter, CreateTableAsync, Router, ListTitle, CreateTitle);
        return await Task.FromResult(model);
    }

    protected abstract IHtmlContent CreateEditor(ViewContext viewContext, TEntity entity);

    protected virtual async Task<IHtmlContent> CreateEditorAsync(ViewContext viewContext, TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(viewContext);
        ArgumentNullException.ThrowIfNull(entity);

        IHtmlContent editor = CreateEditor(viewContext, entity);

        return await Task.FromResult(editor);
    }

    protected abstract IHtmlContent CreateTable(ViewContext viewContext, IPaginatedList<CrudItemModel<TEntity>> model);

    protected virtual async Task<IHtmlContent> CreateTableAsync(ViewContext viewContext, IPaginatedList<CrudItemModel<TEntity>> model)
    {
        ArgumentNullException.ThrowIfNull(viewContext);
        ArgumentNullException.ThrowIfNull(model);

        IHtmlContent table = CreateTable(viewContext, model);

        return await Task.FromResult(table);
    }
}
