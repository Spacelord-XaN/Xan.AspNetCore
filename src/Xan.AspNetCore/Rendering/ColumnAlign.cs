using System.Diagnostics;

namespace Xan.AspNetCore.Rendering;

public enum ColumnAlign
{
    Center,
    Left,
    Right
}

public static class ColumnAlignExtensions
{
    public static string ToHtmlString(this ColumnAlign align)
        => align switch
    {
        ColumnAlign.Center => "center",
        ColumnAlign.Left => "left",
        ColumnAlign.Right => "right",
        _ => throw new UnreachableException(),
    };

    public static string GetStyle(this ColumnAlign align)
        => $"text-align: {align.ToHtmlString()};";
}
