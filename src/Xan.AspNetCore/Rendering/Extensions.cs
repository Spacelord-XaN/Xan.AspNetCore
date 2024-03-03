using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Localization;

namespace Xan.AspNetCore.Rendering;

public static class Extensions
{
    public static IHtmlContent ToHtmlDisplay(this int value)
        => new HtmlString(value.ToString());

    public static IHtmlContent ToHtmlDisplay(this int? value)
    {
        if (value.HasValue)
        {
            return value.Value.ToHtmlDisplay();
        }
        return new HtmlString(string.Empty);
    }

    public static IHtmlContent ToHtmlDisplay(this double value)
        => new HtmlString(value.ToString());

    public static IHtmlContent ToHtmlDisplay(this double? value)
    {
        if (value.HasValue)
        {
            return value.Value.ToHtmlDisplay();
        }
        return new HtmlString(string.Empty);
    }

    public static IHtmlContent ToHtmlDisplay(this decimal value)
        => new HtmlString(value.ToString());

    public static IHtmlContent ToHtmlDisplay(this decimal? value)
    {
        if (value.HasValue)
        {
            return value.Value.ToHtmlDisplay();
        }
        return new HtmlString(string.Empty);
    }

    public static IHtmlContent ToHtmlDisplay(this string? value)
    {
        if (value is not null)
        {
            return new HtmlString(value);
        }
        return new HtmlString(string.Empty);
    }

    public static IHtmlContent ToHtml(this LocalizedString localizedString)
    {
        ArgumentNullException.ThrowIfNull(localizedString);

        return new HtmlString(localizedString.Value);
    }

    public static IHtmlContent ToHtmlDateDisplay(this DateTime value)
        => new HtmlString(value.ToString("D"));

    public static IHtmlContent ToHtmlDateDisplay(this DateTime? value)
    {
        if (value.HasValue)
        {
            return value.Value.ToHtmlDateDisplay();
        }

        return new HtmlString(string.Empty);
    }

    public static IHtmlContent ToHtmlTimeStampDisplay(this DateTime value)
        => new HtmlString(value.ToString("g"));

    public static IHtmlContent ToHtmlTimeStampDisplay(this DateTime? value)
    {
        if (value.HasValue)
        {
            return value.Value.ToHtmlTimeStampDisplay();
        }

        return new HtmlString(string.Empty);
    }

    public static IHtmlContent ToHtmlPriceDisplay(this decimal value)
        => new HtmlString(value.ToString("c"));

    public static IHtmlContent ToHtmlPriceDisplay(this decimal? value)
    {
        if (value.HasValue)
        {
            return value.Value.ToHtmlPriceDisplay();
        }
        return new HtmlString(string.Empty);
    }
}
