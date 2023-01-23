using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Localization;

namespace Xan.AspNetCore.Rendering;

public sealed class FooterBuilder
{
    private readonly ColumnFooterConfig _config = new();

    public FooterBuilder(IHtmlFactory html, IStringLocalizer localizer)
    {
        Html = html ?? throw new ArgumentNullException(nameof(html));
        Localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
    }

    public IHtmlFactory Html { get; }

    public IStringLocalizer Localizer { get; }

    public FooterBuilder Align(ColumnAlign align)
    {
        _config.Align = align;
        return this;
    }

    public ColumnFooterConfig Build()
        => _config;

    public FooterBuilder For(IHtmlContent content)
    {
        _config.Content = content ?? throw new ArgumentNullException(nameof(content));
        return this;
    }
}
