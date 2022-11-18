using System.Diagnostics;

namespace Xan.AspNetCore.Rendering;

public enum ColumnAlign
{
    Right,
    Left
}

public static class ColumnAlignExtensions
{
    public static string ToHtmlString(this ColumnAlign align)
        => align switch
    {
        ColumnAlign.Right => "right",
        ColumnAlign.Left => "left",
        _ => throw new UnreachableException(),
    };

    public static string GetStyle(this ColumnAlign align)
        => $"text-align: {align.ToHtmlString()};";
}
