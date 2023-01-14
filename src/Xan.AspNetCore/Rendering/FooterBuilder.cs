using Microsoft.AspNetCore.Html;

namespace Xan.AspNetCore.Rendering;

public sealed class FooterBuilder<TItem>
{
    private readonly ColumnFooterConfig _config;
    private readonly ColumnBuilder<TItem> _columnBuilder;

    public FooterBuilder(ColumnFooterConfig config, ColumnBuilder<TItem> columnBuilder)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _columnBuilder = columnBuilder ?? throw new ArgumentNullException(nameof(columnBuilder));
    }

    public FooterBuilder<TItem> Align(ColumnAlign align)
    {
        _config.Align = align;
        return this;
    }

    public ColumnBuilder<TItem> For(IHtmlContent content)
    {
        _config.Content = content ?? throw new ArgumentNullException(nameof(content));
        return _columnBuilder;
    }
}
