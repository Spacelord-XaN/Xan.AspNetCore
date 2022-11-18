using Microsoft.AspNetCore.Html;

namespace Xan.AspNetCore.Rendering;

public sealed class ColumnFooterConfig
{
    public ColumnAlign Align { get; set; } = ColumnAlign.Left;

    public IHtmlContent Content { get; set; } = new HtmlString(string.Empty);

    public string GetStyle()
        => Align.GetStyle();
}
