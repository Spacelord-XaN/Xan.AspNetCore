using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore.Rendering;

public static class ColumnBuilderExtensions
{
    public static ColumnBuilder<T> For<T>(this ColumnBuilder<T> builder, Func<T, int?> getInt)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getInt);

        return builder.For(item => getInt(item).ToHtmlDisplay());
    }

    public static ColumnBuilder<T> For<T>(this ColumnBuilder<T> builder, Func<T, double?> getDouble)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getDouble);

        return builder.For(item => getDouble(item).ToHtmlDisplay());
    }

    public static ColumnBuilder<T> For<T>(this ColumnBuilder<T> builder, Func<T, decimal?> getDecimal)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getDecimal);

        return builder.For(item => getDecimal(item).ToHtmlDisplay());
    }

    public static ColumnBuilder<T> For<T>(this ColumnBuilder<T> builder, Func<T, string?> getString)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getString);

        return builder.For(item => getString(item).ToHtmlDisplay());
    }

    public static ColumnBuilder<T> For<T>(this ColumnBuilder<T> builder, Func<T, DateTime?> getDateTime)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getDateTime);

        return builder.For(item => getDateTime(item).ToHtmlTimeStampDisplay());
    }

    public static ColumnBuilder<CrudItemModel<T>> ForEditLink<T>(this ColumnBuilder<CrudItemModel<T>> builder, ICrudRouter router)
        where T : IEntity
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(router);

        return builder.ForLink(item => router.ToEdit(item.Entity.Id), builder.Localizer[XanAspNetCoreTexts.Edit]);
    }

    public static ColumnBuilder<T> ForEditLink<T>(this ColumnBuilder<T> builder, ICrudRouter router)
        where T : IEntity
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(router);

        return builder.ForLink(item => router.ToEdit(item.Id), builder.Localizer[XanAspNetCoreTexts.Edit]);
    }

    public static ColumnBuilder<T> ForLink<T>(this ColumnBuilder<T> builder, Func<T, string> getUrl, string text)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getUrl);
        ArgumentNullException.ThrowIfNull(text);

        return builder
            .ForLink(getUrl, item => text.ToHtmlDisplay());
    }

    public static ColumnBuilder<T> ForLink<T>(this ColumnBuilder<T> builder, Func<T, string> getUrl, Func<T, string> getText)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getUrl);
        ArgumentNullException.ThrowIfNull(getText);

        return builder
            .ForLink(getUrl, item => getText(item).ToHtmlDisplay(), item => true);
    }

    public static ColumnBuilder<T> ForLink<T>(this ColumnBuilder<T> builder, Func<T, string> getUrl, Func<T, IHtmlContent> getContent)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getUrl);
        ArgumentNullException.ThrowIfNull(getContent);

        return builder
            .ForLink(getUrl, getContent, item => true);
    }

    public static ColumnBuilder<T> ForLink<T>(this ColumnBuilder<T> builder, Func<T, string> getUrl, string text, Func<T, bool> isVisible)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getUrl);
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(isVisible);

        return builder
            .ForLink(getUrl, item => text.ToHtmlDisplay(), isVisible);
    }

    public static ColumnBuilder<T> ForLink<T>(this ColumnBuilder<T> builder, Func<T, string> getUrl, Func<T, IHtmlContent> getContent, Func<T, bool> isVisible)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getUrl);
        ArgumentNullException.ThrowIfNull(getContent);
        ArgumentNullException.ThrowIfNull(isVisible);

        return builder
            .For(item =>
            {
                if (isVisible(item))
                {
                    string url = getUrl(item);
                    TagBuilder link = builder.Html.Link(url);
                    link.InnerHtml.SetHtmlContent(getContent(item));
                    return link;
                }
                else
                {
                    return new HtmlString(string.Empty);
                }
            });
    }

    public static ColumnBuilder<T> ForPrice<T>(this ColumnBuilder<T> builder, Func<T, decimal?> getPrice)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(getPrice);

        return builder.For(item => getPrice(item).ToHtmlPriceDisplay());
    }

    public static ColumnBuilder<T> Title<T>(this ColumnBuilder<T> builder, string title)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(title);

        return builder.Title(title.ToHtmlDisplay());
    }
}
