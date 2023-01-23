namespace Xan.AspNetCore.Models;

#nullable disable warnings
public sealed class SelectModel<T>
{
    public SelectModel()
    { }

    public SelectModel(T value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public bool IsSelected { get; set; }

    public T Value { get; set; }
}

public static class SelectModelExtensions
{
    public static IEnumerable<T> SelectedModels<T>(this IEnumerable<SelectModel<T>> selectModels)
    {
        ArgumentNullException.ThrowIfNull(selectModels);

        return selectModels.Where(sm => sm.IsSelected)
            .Select(sm => sm.Value);
    }

    public static IEnumerable<SelectModel<T>> ToSelectedModels<T>(this IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        return items.Select(item => new SelectModel<T>(item));
    }
}
