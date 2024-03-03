using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class TestBase
{
    protected DefaultHtmlFactory Sut { get; } = new (new StringLocalizerMock());
}
