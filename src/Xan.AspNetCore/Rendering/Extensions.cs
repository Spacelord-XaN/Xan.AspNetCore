using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Localization;

namespace Xan.AspNetCore.Rendering;

public static class Extensions
{
    public static IHtmlContent ToHtml(this int? value)
        => new HtmlString(value.ToString());

    public static IHtmlContent ToHtml(this LocalizedString localizedString)
    {
        ArgumentNullException.ThrowIfNull(localizedString);

        return new HtmlString(localizedString.Value);
    }

    public static IHtmlContent ToHtml(this string? value)
        => new HtmlString(value ?? "");

    public static IHtmlContent ToHtmlDate(this DateTime value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return new HtmlString($"{value:D}");
    }

    public static IHtmlContent ToHtmlTimeStamp(this DateTime value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return new HtmlString($"{value:g}");
    }

    public static IHtmlContent ToHtmlTimeStamp(this DateTime? value)
    {
        if (value.HasValue)
        {
            return value.Value.ToHtmlTimeStamp();
        }

        return new HtmlString(string.Empty);
    }

    public static IHtmlContent ToHtmlPrice(this decimal value)
        => new HtmlString($"{value:c}");

    public static IHtmlContent ToHtmlPrice(this decimal? value)
    {
        if (value.HasValue)
        {
            return ToHtmlPrice(value.Value);
        }
        return new HtmlString(string.Empty);
    }
}
