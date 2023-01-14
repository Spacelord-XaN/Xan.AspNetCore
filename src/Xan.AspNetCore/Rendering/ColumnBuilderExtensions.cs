using Microsoft.Extensions.Localization;

namespace Xan.AspNetCore.Rendering;

public static class ColumnBuilderExtensions
{
    public static TableBuilder<TItem> For<TItem>(this ColumnBuilder<TItem> builder, Func<TItem, int?> getInt)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getInt);

        return builder.For(item => getInt(item).ToHtml());
    }

    public static TableBuilder<TItem> For<TItem>(this ColumnBuilder<TItem> builder, Func<TItem, string?> getString)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getString);

        return builder.For(item => getString(item).ToHtml());
    }

    public static TableBuilder<TItem> ForPrice<TItem>(this ColumnBuilder<TItem> builder, Func<TItem, decimal?> getPrice)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getPrice);

        return builder.For(item => getPrice(item).ToHtmlPrice());
    }

    public static TableBuilder<TItem> ForTimeStamp<TItem>(this ColumnBuilder<TItem> builder, Func<TItem, DateTime?> getTimeStamp)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getTimeStamp);

        return builder.For(item => getTimeStamp(item).ToHtmlTimeStamp());
    }

    public static ColumnBuilder<TItem> Title<TItem>(this ColumnBuilder<TItem> builder, LocalizedString title)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(title);

        return builder.Title(title.ToHtml());
    }
}
