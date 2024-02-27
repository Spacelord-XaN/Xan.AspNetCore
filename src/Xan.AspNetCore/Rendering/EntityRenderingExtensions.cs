using Microsoft.AspNetCore.Html;
using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.Rendering;

public static class EntityRenderingExtensions
{
    public static IHtmlContent HiddenIEntityInput(this IHtmlFactory factory, IEntity entity)
    {
        ArgumentNullException.ThrowIfNull(factory);
        ArgumentNullException.ThrowIfNull(entity);

        return factory.HiddenInput(nameof(IEntity.Id), entity.Id);
    }

    public static IHtmlContent HiddenIHasTimestampsInput(this IHtmlFactory factory, IHasTimestamps entity)
    {
        ArgumentNullException.ThrowIfNull(factory);
        ArgumentNullException.ThrowIfNull(entity);

        return factory.HiddenInput(nameof(IHasTimestamps.CreatedAt), entity.CreatedAt);
    }
}
