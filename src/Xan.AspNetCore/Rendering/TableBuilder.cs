using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xan.AspNetCore.Rendering;

public sealed class TableBuilder<TItem>
    : List<ColumnConfig<TItem>>
{
    private readonly IEnumerable<TItem> _items;
    private readonly IHtmlFactory _html;

    public TableBuilder(IEnumerable<TItem> items, IHtmlFactory html)
    {
        _items = items ?? throw new ArgumentNullException(nameof(items));
        _html = html ?? throw new ArgumentNullException(nameof(html));
    }

    public IHtmlContent Build()
    {
        TagBuilder table = _html.Table();

        IHtmlContent header = CreateHeader();
        table.InnerHtml.AppendHtml(header);

        IHtmlContent body = CreateBody();
        table.InnerHtml.AppendHtml(body);

        if (this.Any(c => c.HasFooter))
        {
            IHtmlContent footer = CreateFooter();
            table.InnerHtml.AppendHtml(footer);
        }

        return table;
    }

    private IHtmlContent CreateHeader()
    {
        IHtmlContent cells = CreateHeaderCells();

        TagBuilder tr = _html.Tr();
        tr.InnerHtml.SetHtmlContent(cells);

        TagBuilder tHead = _html.THead();
        tHead.InnerHtml.SetHtmlContent(tr);

        return tHead;
    }

    private IHtmlContent CreateHeaderCells()
    {
        HtmlContentBuilder cells = new();
        foreach (ColumnConfig<TItem> config in this)
        {
            IHtmlContent cell = CreateHeaderCell(config);
            cells.AppendHtml(cell);
        }
        return cells;
    }

    private IHtmlContent CreateHeaderCell(ColumnConfig<TItem> config)
    {
        TagBuilder th = _html.Th(TableScope.Col);
        th.SetStyle(config.GetStyle());
        th.InnerHtml.SetHtmlContent(config.Title);
        return th;
    }

    private IHtmlContent CreateBody()
    {
        TagBuilder tBody = _html.TBody();

        int index = 0;
        foreach (TItem item in _items)
        {
            if (item == null)
            {
                throw new InvalidOperationException("Item in collection cannot be null.");
            }
            IHtmlContent cells = CreateBodyCells(index, item);

            TagBuilder tr = _html.Tr();
            tr.InnerHtml.SetHtmlContent(cells);

            tBody.InnerHtml.AppendHtml(tr);
            index++;
        }

        return tBody;
    }

    private IHtmlContent CreateBodyCells(int index, TItem item)
    {
        HtmlContentBuilder cells = new();
        foreach (ColumnConfig<TItem> config in this)
        {
            IHtmlContent cell = CreateBodyCell(index, item, config);
            cells.AppendHtml(cell);
        }
        return cells;
    }

    private IHtmlContent CreateBodyCell(int index, TItem item, ColumnConfig<TItem> config)
    {
        TagBuilder td = _html.Td(TableScope.None);
        td.SetStyle(config.GetStyle());
        td.InnerHtml.SetHtmlContent(config.GetContent(index, item));

        return td;
    }

    private IHtmlContent CreateFooter()
    {
        TagBuilder tr = _html.Tr();
        foreach (ColumnConfig<TItem> config in this)
        {
            tr.InnerHtml.AppendHtml(GetFooter(config));
        }

        TagBuilder tFoot = _html.TFoot();
        tFoot.InnerHtml.SetHtmlContent(tr);

        return tFoot;
    }

    public IHtmlContent GetFooter(ColumnConfig<TItem> config)
    {
        TagBuilder th = _html.Td(TableScope.Col);
        if (config.Footer != null)
        {
            th.SetStyle(config.Footer.GetStyle());
            th.InnerHtml.SetHtmlContent(config.Footer.Content);
        }
        return th;
    }
}
