using Microsoft.Extensions.Localization;

namespace Xan.AspNetCore.Mvc.ViewFeatures;

public static class ViewDataDictionaryExtensions
{
    internal const string _titleKey = "Title";

    public static void SetTitle(this IDictionary<string, object?> viewData, LocalizedString value)
        => SetData(viewData, _titleKey, value);

    public static LocalizedString Title(this IDictionary<string, object?> viewData)
    {
        LocalizedString? title = GetData<LocalizedString>(viewData, _titleKey);
        if (title is null)
        {
            throw new InvalidOperationException("No page title set.");
        }
        return title;
    }

    private static T? GetData<T>(this IDictionary<string, object?> viewData, string key)
    {
        ArgumentNullException.ThrowIfNull(viewData);
        ArgumentNullException.ThrowIfNull(key);

        if (viewData.TryGetValue(key, out object? data))
        {
            if (data is not null)
            {
                return (T)data;
            }
        }
        return default;
    }

    private static void SetData<T>(this IDictionary<string, object?> viewData, string key, T data)
    {
        ArgumentNullException.ThrowIfNull(viewData);
        ArgumentNullException.ThrowIfNull(key);

        if (viewData.ContainsKey(key))
        {
            viewData[key] = data;
        }
        else
        {
            viewData.Add(key, data);
        }
    }
}
