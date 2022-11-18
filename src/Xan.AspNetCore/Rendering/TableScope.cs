using System.Diagnostics;

namespace Xan.AspNetCore.Rendering;

public enum TableScope
{
    None,
    Col
}

public static class TableScopeExtensions
{
    public static string ToHtmlAttributeString(this TableScope scope)
        => scope switch
        {
            TableScope.None => "",
            TableScope.Col => "col",
            _ => throw new UnreachableException(),
        };
}
