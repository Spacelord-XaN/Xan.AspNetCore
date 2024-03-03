using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.TableBuilderExtensionsTests;

public class TestBase
{
    protected static TableBuilder<T> CreateSut<T>(IEnumerable<T> items)
    {
        StringLocalizerMock localizer = new();
        return new(items, new DefaultHtmlFactory(localizer), localizer);
    }
}
