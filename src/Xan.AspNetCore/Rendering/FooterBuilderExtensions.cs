using Microsoft.Extensions.Localization;

namespace Xan.AspNetCore.Rendering;

public static class FooterBuilderExtensions
{
    public static ColumnBuilder<TItem> For<TItem>(this FooterBuilder<TItem> builder, LocalizedString text)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(text);

        return builder.For(text.ToHtml());
    }
}
