using Microsoft.AspNetCore.Html;

namespace Xan.AspNetCore.Rendering;

public sealed class ColumnConfig<TItem>
{
    private Func<int, TItem, IHtmlContent>? _getContent;

    public bool HasFooter { get => Footer != null; }

    public bool DoNotBeak { get; set; }

    public ColumnAlign Align { get; set; } = ColumnAlign.Left;

    public ColumnFooterConfig? Footer { get; set; }

    public Width Width { get; set; } = Width.Auto;

    public IHtmlContent Title { get; set; } = new HtmlString(string.Empty);

    public ColumnConfig<TItem> SetContent(Func<TItem, IHtmlContent> getContent)
    {
        ArgumentNullException.ThrowIfNull(getContent);

        return SetContent((Index, item) => getContent(item));
    }

    public ColumnConfig<TItem> SetContent(Func<int, TItem, IHtmlContent> getContent)
    {
        ArgumentNullException.ThrowIfNull(getContent);

        _getContent = getContent;

        return this;
    }

    public IHtmlContent GetContent(int index, TItem item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (_getContent == null)
        {
            throw new InvalidOperationException("No content getter was provided");
        }
        return _getContent(index, item);
    }

    public string GetStyle()
    {
        string style = string.Empty;
        if (Width != null)
        {
            style += Width.GetStyle();
        }
        style += Align.GetStyle();
        if (DoNotBeak)
        {
            style += "white-space: nowrap;";
        }
        else
        {
            style += "word-wrap: break-word;max-width: 1px;";
        }
        return style;
    }
}
