using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xan.AspNetCore.Rendering;

public static class TagBuilderExtensions
{
    public static void SetAutoFocus(this TagBuilder tagBuilder)
    {
        ArgumentNullException.ThrowIfNull(tagBuilder);

        tagBuilder.Attributes.Add("autofocus", null);
    }

    public static void SetName(this TagBuilder tagBuilder, string name)
    {
        ArgumentNullException.ThrowIfNull(tagBuilder);
        ArgumentNullException.ThrowIfNull(name);

        tagBuilder.Attributes.Add("name", name);
    }

    public static void SetId(this TagBuilder tagBuilder, string id)
    {
        ArgumentNullException.ThrowIfNull(tagBuilder);
        ArgumentNullException.ThrowIfNull(id);

        tagBuilder.Attributes.Add("id", id);
    }

    public static void SetScope(this TagBuilder tagBuilder, string scope)
    {
        ArgumentNullException.ThrowIfNull(tagBuilder);
        ArgumentNullException.ThrowIfNull(scope);

        tagBuilder.Attributes.Add("scope", scope);
    }

    public static void SetStyle(this TagBuilder tagBuilder, string style)
    {
        ArgumentNullException.ThrowIfNull(tagBuilder);
        ArgumentNullException.ThrowIfNull(style);

        tagBuilder.Attributes.Add("style", style);
    }
}
