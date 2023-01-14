using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace Xan.AspNetCore.Rendering;

public static class TableBuilderExtensions
{
    public static TableBuilder<TItem> Add<TItem>(this TableBuilder<TItem> builder, Action<ColumnConfig<TItem>> configure)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configure);

        ColumnConfig<TItem> config = new();
        configure(config);
        builder.Add(config);

        return builder;
    }

    public static TableBuilder<TItem> Add<TItem>(this TableBuilder<TItem> builder, Func<TItem, IHtmlContent> getContent)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getContent);

        ColumnConfig<TItem> config = new();
        config.SetContent(getContent);
        builder.Add(config);
        return builder;
    }

    public static TableBuilder<TItem> Add<TItem>(this TableBuilder<TItem> builder, Width width, Func<TItem, IHtmlContent> getContent)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getContent);

        ColumnConfig<TItem> config = new()
        {
            Width = width ?? throw new ArgumentNullException(nameof(width))
        };
        config.SetContent(getContent);
        builder.Add(config);
        return builder;
    }

    public static TableBuilder<TItem> Add<TItem>(this TableBuilder<TItem> builder, Width width, LocalizedString title, Func<TItem, IHtmlContent> getContent)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getContent);
        ArgumentNullException.ThrowIfNull(title);

        ColumnConfig<TItem> config = new()
        {
            Width = width ?? throw new ArgumentNullException(nameof(width)),
            Title = title.ToHtml()
        };
        config.SetContent(getContent);
        builder.Add(config);
        return builder;
    }

    public static TableBuilder<TItem> Add<TItem>(this TableBuilder<TItem> builder, Width width, LocalizedString title, Func<TItem, int> getValue)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(width);
        ArgumentNullException.ThrowIfNull(title);
        ArgumentNullException.ThrowIfNull(getValue);

        return builder.Add(width, title, item => getValue(item).ToHtml());
    }

    public static TableBuilder<TItem> Add<TItem>(this TableBuilder<TItem> builder, Width width, LocalizedString title, Func<TItem, string?> getValue)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(width);
        ArgumentNullException.ThrowIfNull(title);
        ArgumentNullException.ThrowIfNull(getValue);

        return builder.Add(width, title, item => getValue(item).ToHtml());
    }

    public static TableBuilder<TItem> AddLink<TItem>(this TableBuilder<TItem> builder, LocalizedString text, Func<TItem, string> getUrl, Func<TItem, bool>? isUrlEnabled = null)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(getUrl);

        return builder.AddLink(Width.Auto, new LocalizedString(string.Empty, string.Empty), item => text.ToHtml(), getUrl, isUrlEnabled);
    }

    public static TableBuilder<TItem> AddLink<TItem>(this TableBuilder<TItem> builder, Width width, LocalizedString title, Func<TItem, IHtmlContent> getContent, Func<TItem, string> getUrl, Func<TItem, bool>? isUrlEnabled = null)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getContent);
        ArgumentNullException.ThrowIfNull(getUrl);
        ArgumentNullException.ThrowIfNull(title);

        ColumnConfig<TItem> config = new()
        {
            Width = width ?? throw new ArgumentNullException(nameof(width)),
            Title = title.ToHtml(),
            DoNotBeak = true
        };
        config.SetContent(item =>
        {
            bool enabled = true;
            if (isUrlEnabled != null)
            {
                enabled = isUrlEnabled(item);
            }

            if (enabled)
            {
                string url = getUrl(item);
                TagBuilder a = new("a");
                a.Attributes.Add("href", url);
                a.InnerHtml.SetHtmlContent(getContent(item));
                return a;
            }
            else
            {
                return new HtmlString(string.Empty);
            }
        });
        builder.Add(config);

        return builder;
    }

    public static TableBuilder<TItem> Insert<TItem>(this TableBuilder<TItem> builder, int index, Action<ColumnConfig<TItem>> configure)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configure);

        ColumnConfig<TItem> config = new();
        configure(config);
        builder.Insert(index, config);

        return builder;
    }
}
