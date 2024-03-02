using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.Mvc.Crud;

public sealed class CrudModel<TEntity>(TEntity entity, CrudModel<TEntity>.CreateEditorDelegate createEditorAsync, string title)
    : ICrudModel
    where TEntity : class, ICrudEntity, new()
{
    public delegate Task<IHtmlContent> CreateEditorDelegate(ViewContext viewContext, TEntity entity);

    public TEntity Entity { get; } = entity ?? throw new ArgumentNullException(nameof(entity));

    public string Title { get; } = title ?? throw new ArgumentNullException(nameof(title));

    public async Task<IHtmlContent> EditorAsync(ViewContext viewContext)
    {
        ArgumentNullException.ThrowIfNull(viewContext);

        return await createEditorAsync(viewContext, Entity);
    }
}
