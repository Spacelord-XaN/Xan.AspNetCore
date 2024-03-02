using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Localization;

namespace Xan.AspNetCore.Rendering;

public sealed class ColumnBuilder<T>(IHtmlFactory html, IStringLocalizer localizer)
{
    private readonly ColumnConfig<T> _config = new();

    public IHtmlFactory Html { get; } = html ?? throw new ArgumentNullException(nameof(html));

    public IStringLocalizer Localizer { get; } = localizer ?? throw new ArgumentNullException(nameof(localizer));

    public ColumnBuilder<T> Align(ColumnAlign align)
    {
        _config.Align = align;
        return this;
    }

    public ColumnBuilder<T> AutoWidth()
    {
        _config.Width = Width.Auto;
        return this;
    }

    public ColumnConfig<T> Build()
        => _config;

    public ColumnBuilder<T> BreakText()
    {
        _config.BreakText = true;
        return this;
    }

    public ColumnBuilder<T> For(Func<T, IHtmlContent> getContent)
    {
        ArgumentNullException.ThrowIfNull(getContent);

        _config.SetContent(getContent);
        return this;
    }

    public ColumnBuilder<T> For(Func<int, T, IHtmlContent> getContent)
    {
        ArgumentNullException.ThrowIfNull(getContent);

        _config.SetContent(getContent);
        return this;
    }

    public ColumnBuilder<T> Footer(Action<FooterBuilder> configureFooter)
    {
        FooterBuilder builder = new(Html, Localizer);
        configureFooter(builder);
        _config.Footer = builder.Build();
        return this;
    }

    public ColumnBuilder<T> PercentWidth(int percentage)
    {
        _config.Width = Width.Percent(percentage);
        return this;
    }

    public ColumnBuilder<T> Title(IHtmlContent title)
    {
        _config.Title = title ?? throw new ArgumentNullException(nameof(title));
        return this;
    }
}
