namespace Xan.AspNetCore.Tests;

public static class X
{
    public static T StrictFake<T>()
        where T : class
        => A.Fake<T>(options => options.Strict());

    public static string Line(this string s, string nextLine = "")
    {
        ArgumentNullException.ThrowIfNull(s);
        ArgumentNullException.ThrowIfNull(nextLine);

        return s + Environment.NewLine + nextLine;
    }
}
