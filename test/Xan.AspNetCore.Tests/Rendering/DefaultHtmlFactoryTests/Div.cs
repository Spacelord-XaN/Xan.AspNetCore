using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Div
{
    [Fact]
    public void ReturnsHtml()
    {
        DefaultHtmlFactory sut = new(new StringLocalizerMock());

        TagBuilder div = sut.Div();
        div.Should().BeHtml("<div></div>");
    }
}
