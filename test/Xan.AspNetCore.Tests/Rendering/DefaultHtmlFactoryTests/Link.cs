using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Link
    : TestBase
{
    [Theory]
    [AutoData]
    public void ReturnsHtml(string url)
    {
        //  Arrange

        //  Act
        TagBuilder div = Sut.Link(url);
        
        //  Assert
        div.Should().BeHtml($"""<a href="{url}"></a>""");
    }
}
