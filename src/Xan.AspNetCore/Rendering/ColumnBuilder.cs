using Microsoft.AspNetCore.Html;

namespace Xan.AspNetCore.Rendering;

public sealed class ColumnBuilder<TItem>
{
    private readonly ColumnConfig<TItem> _config;
    private readonly TableBuilder<TItem> _tableBuidler;

    public ColumnBuilder(ColumnConfig<TItem> config, TableBuilder<TItem> tableBuidler)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _tableBuidler = tableBuidler ?? throw new ArgumentNullException(nameof(tableBuidler));
    }

    public IHtmlFactory Html { get => _tableBuidler.Html; }

    public ColumnBuilder<TItem> Align(ColumnAlign align)
    {
        _config.Align = align;
        return this;
    }

    public ColumnBuilder<TItem> AutoWidth()
    {
        _config.Width = Width.Auto;
        return this;
    }

    public ColumnBuilder<TItem> DoNotBreak()
    {
        _config.DoNotBeak = true;
        return this;
    }

    public TableBuilder<TItem> For(Func<TItem, IHtmlContent> getContent)
    {
        ArgumentNullException.ThrowIfNull(getContent);

        _config.SetContent(getContent);
        return _tableBuidler;
    }

    public TableBuilder<TItem> For(Func<int, TItem, IHtmlContent> getContent)
    {
        ArgumentNullException.ThrowIfNull(getContent);

        _config.SetContent(getContent);
        return _tableBuidler;
    }

    public FooterBuilder<TItem> Footer()
    {
        _config.Footer = new ColumnFooterConfig();
        return new(_config.Footer, this);
    }

    public ColumnBuilder<TItem> PercentWidth(int percentage)
    {
        _config.Width = Width.Percent(percentage);
        return this;
    }

    public ColumnBuilder<TItem> Title(IHtmlContent title)
    {
        _config.Title = title ?? throw new ArgumentNullException(nameof(title));
        return this;
    }
}
