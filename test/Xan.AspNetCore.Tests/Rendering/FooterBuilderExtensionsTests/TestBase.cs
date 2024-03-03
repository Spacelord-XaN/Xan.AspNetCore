using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.FooterBuilderExtensionsTests;

public class TestBase
{
    public TestBase()
    {
        StringLocalizerMock localizer = new();
        Sut = new(new DefaultHtmlFactory(localizer), localizer);
    }

    public FooterBuilder Sut { get; }
}
