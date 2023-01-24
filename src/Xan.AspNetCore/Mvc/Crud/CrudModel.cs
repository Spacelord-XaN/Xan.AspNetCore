using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.Mvc.Crud;

public sealed class CrudModel<TEntity>
    : ICrudModel
    where TEntity : class, ICrudEntity, new()
{
    public delegate Task<IHtmlContent> CreateEditorDelegate(ViewContext viewContext, TEntity entity);

    private readonly CreateEditorDelegate _createEditorAsync;

    public CrudModel(TEntity entity, CreateEditorDelegate createEditorAsync, string title)
    {
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        _createEditorAsync = createEditorAsync ?? throw new ArgumentNullException(nameof(createEditorAsync));
        Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    public TEntity Entity { get; }

    public string Title { get; }

    public async Task<IHtmlContent> EditorAsync(ViewContext viewContext)
    {
        ArgumentNullException.ThrowIfNull(viewContext);

        return await _createEditorAsync(viewContext, Entity);
    }
}
