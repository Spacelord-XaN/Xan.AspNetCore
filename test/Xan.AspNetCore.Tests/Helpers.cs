namespace Xan.AspNetCore.Tests;

public static class Helpers
{
    public static T Fake<T>()
        where T : class
    {
        return A.Fake<T>(options => options.Strict());
    }
}
