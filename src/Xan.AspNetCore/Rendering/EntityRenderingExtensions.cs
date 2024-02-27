using Microsoft.AspNetCore.Html;
using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.Rendering;

public static class EntityRenderingExtensions
{
    public static HtmlContentBuilder HiddenInputs(this IHtmlFactory factory, IEntity entity)
    {
        ArgumentNullException.ThrowIfNull(factory);
        ArgumentNullException.ThrowIfNull(entity);

        HtmlContentBuilder builder = new();
        builder.AppendHtml(factory.HiddenInput(nameof(IEntity.Id), entity.Id));
        return builder;
    }

    public static HtmlContentBuilder HiddenInputs(this IHtmlFactory factory, IHasTimestamps entity)
    {
        ArgumentNullException.ThrowIfNull(factory);
        ArgumentNullException.ThrowIfNull(entity);

        HtmlContentBuilder builder = new();
        builder.AppendHtml(factory.HiddenInput(nameof(IHasTimestamps.CreatedAt), entity.CreatedAt));
        return builder;
    }

    public static HtmlContentBuilder HiddenInputs(this IHtmlFactory factory, ICrudEntity entity)
    {
        ArgumentNullException.ThrowIfNull(factory);
        ArgumentNullException.ThrowIfNull(entity);

        HtmlContentBuilder builder = new();
        builder.AppendHtml(factory.HiddenInput(nameof(IEntity.Id), entity.Id));
        builder.AppendHtml(factory.HiddenInput(nameof(IHasTimestamps.CreatedAt), entity.CreatedAt));
        return builder;
    }
}
