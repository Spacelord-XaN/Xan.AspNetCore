using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Globalization;
using System.Text;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Http;

public static class PageSizeCookie
{
    public const string Key = "pageSize";
    
    public static CookieOptions Options { get; } = new CookieOptions
    {
        HttpOnly = true,
        IsEssential = false,
        SameSite = SameSiteMode.Strict,
        Secure = false
    };
    
    private static readonly CookieConfig _pageSize = new(Key, Options);

    public static bool HasPageSize(this IRequestCookieCollection cookies, string key)
    {
        ArgumentNullException.ThrowIfNull(cookies);
        ArgumentNullException.ThrowIfNull(key);

        return cookies.ContainsKey(key);
    }

    public static int GetPageSize(this IRequestCookieCollection cookies, string key)
    {
        ArgumentNullException.ThrowIfNull(cookies);
        ArgumentNullException.ThrowIfNull(key);

        string? id = cookies[key];
        if (int.TryParse(id, out int pageSize))
        {
            return pageSize;
        }
        return ListParameter.DefaultPageSize;
    }

    public static void SetPageSize(this IResponseCookies cookies, string key, int pageSize)
    {
        ArgumentNullException.ThrowIfNull(cookies);
        ArgumentNullException.ThrowIfNull(key);

        cookies.Append(key, pageSize.ToString(CultureInfo.InvariantCulture), _pageSize.CookieOptions);
    }

    public static bool HasPageSize(this IRequestCookieCollection cookies, RouteValueDictionary routeValues)
    {
        ArgumentNullException.ThrowIfNull(cookies);
        ArgumentNullException.ThrowIfNull(routeValues);

        string key = FindKey(routeValues);
        return cookies.ContainsKey(key);
    }

    public static string FindKey(RouteValueDictionary routeValues)
    {
        ArgumentNullException.ThrowIfNull(routeValues);

        StringBuilder sb = new();
        sb.Append(_pageSize.Key);
        foreach (object? value in routeValues.Values)
        {
            sb.Append('.')
                .Append(value);
        }
        return sb.ToString();
    }
}
