using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Div
{
    [Fact]
    public void ReturnsHtml()
    {
        DefaultHtmlFactory sut = new();

        TagBuilder div = sut.Div();
        div.Should().Html("<div></div>");
    }
}
