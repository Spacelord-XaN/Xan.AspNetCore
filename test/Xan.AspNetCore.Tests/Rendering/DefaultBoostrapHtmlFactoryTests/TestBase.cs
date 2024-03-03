using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.DefaultBoostrapHtmlFactoryTests;

public class TestBase
{
    public TestBase()
    {
        Sut.Contextualize(ViewContext);
    }

    protected DefaultBoostrapHtmlFactory Sut { get; } = new (new StringLocalizerMock());

    protected ViewContext ViewContext { get; } = new();
}
