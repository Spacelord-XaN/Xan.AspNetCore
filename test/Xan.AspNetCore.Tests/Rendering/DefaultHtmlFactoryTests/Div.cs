using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Div
    : TestBase
{
    [Fact]
    public void ReturnsHtml()
    {
        //  Arrange

        //  Act
        TagBuilder div = Sut.Div();
        
        //  Assert
        div.Should().BeHtml("<div></div>");
    }
}
