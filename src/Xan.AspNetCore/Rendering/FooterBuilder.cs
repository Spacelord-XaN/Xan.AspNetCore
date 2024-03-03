using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Localization;

namespace Xan.AspNetCore.Rendering;

public sealed class FooterBuilder(IHtmlFactory html, IStringLocalizer localizer)
{
    private readonly ColumnFooterConfig _config = new();

    public IHtmlFactory Html { get; } = html ?? throw new ArgumentNullException(nameof(html));

    public IStringLocalizer Localizer { get; } = localizer ?? throw new ArgumentNullException(nameof(localizer));

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
