using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Heading
    : TestBase
{
    [Theory]
    [AutoData]
    public void ReturnsHtml(int level)
    {
        //  Arrange

        //  Act
        TagBuilder div = Sut.Heading(level);
        
        //  Assert
        div.Should().BeHtml($"<h{level}></h{level}>");
    }
}
