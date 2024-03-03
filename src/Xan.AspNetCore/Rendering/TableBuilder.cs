using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace Xan.AspNetCore.Rendering;

public class TableBuilder<T>(IEnumerable<T> items, IHtmlFactory html, IStringLocalizer localizer)
    : List<ColumnConfig<T>>
{
    public IHtmlFactory Html { get; } = html ?? throw new ArgumentNullException(nameof(html));

    public IStringLocalizer Localizer { get; } = localizer ?? throw new ArgumentNullException(nameof(localizer));

    public TagBuilder Build()
    {
        TagBuilder table = Html.Table();

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

    public TableBuilder<T> Column(Action<ColumnBuilder<T>> configureColumn)
    {
        ArgumentNullException.ThrowIfNull(configureColumn);

        ColumnBuilder<T> builder = new(Html, Localizer);
        configureColumn(builder);
        Add(builder.Build());
        return this;
    }

    public TableBuilder<T> Column(int index, Action<ColumnBuilder<T>> configureColumn)
    {
        ArgumentNullException.ThrowIfNull(configureColumn);

        ColumnBuilder<T> builder = new(Html, Localizer);
        configureColumn(builder);
        Insert(index, builder.Build());
        return this;
    }

    private TagBuilder CreateHeader()
    {
        IHtmlContent cells = CreateHeaderCells();

        TagBuilder tr = Html.Tr();
        tr.InnerHtml.SetHtmlContent(cells);

        TagBuilder tHead = Html.THead();
        tHead.InnerHtml.SetHtmlContent(tr);

        return tHead;
    }

    private HtmlContentBuilder CreateHeaderCells()
    {
        HtmlContentBuilder cells = new();
        foreach (ColumnConfig<T> config in this)
        {
            IHtmlContent cell = CreateHeaderCell(config);
            cells.AppendHtml(cell);
        }
        return cells;
    }

    private TagBuilder CreateHeaderCell(ColumnConfig<T> config)
    {
        TagBuilder th = Html.Th(TableScope.Col);
        th.SetStyle(config.GetHeaderStyle());
        th.InnerHtml.SetHtmlContent(config.Title);
        return th;
    }

    private TagBuilder CreateBody()
    {
        TagBuilder tBody = Html.TBody();

        int index = 0;
        foreach (T item in items)
        {
            if (item == null)
            {
                throw new InvalidOperationException("Item in collection cannot be null.");
            }
            IHtmlContent cells = CreateBodyCells(index, item);

            TagBuilder tr = Html.Tr();
            tr.InnerHtml.SetHtmlContent(cells);

            tBody.InnerHtml.AppendHtml(tr);
            index++;
        }

        return tBody;
    }

    private HtmlContentBuilder CreateBodyCells(int index, T item)
    {
        HtmlContentBuilder cells = new();
        foreach (ColumnConfig<T> config in this)
        {
            IHtmlContent cell = CreateBodyCell(index, item, config);
            cells.AppendHtml(cell);
        }
        return cells;
    }

    private TagBuilder CreateBodyCell(int index, T item, ColumnConfig<T> config)
    {
        TagBuilder td = Html.Td(TableScope.None);
        td.SetStyle(config.GetStyle());
        td.InnerHtml.SetHtmlContent(config.GetContent(index, item));

        return td;
    }

    private TagBuilder CreateFooter()
    {
        TagBuilder tr = Html.Tr();
        foreach (ColumnConfig<T> config in this)
        {
            tr.InnerHtml.AppendHtml(GetFooter(config));
        }

        TagBuilder tFoot = Html.TFoot();
        tFoot.InnerHtml.SetHtmlContent(tr);

        return tFoot;
    }

    private TagBuilder GetFooter(ColumnConfig<T> config)
    {
        TagBuilder th = Html.Td(TableScope.Col);
        if (config.Footer != null)
        {
            th.SetStyle(config.Footer.GetStyle());
            th.InnerHtml.SetHtmlContent(config.Footer.Content);
        }
        return th;
    }
}
