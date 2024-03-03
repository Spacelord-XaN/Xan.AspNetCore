using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Label
    : TestBase
{
    [Fact]
    public void ReturnsHtml()
    {
        //  Arrange

        //  Act
        TagBuilder div = Sut.Label();
        
        //  Assert
        div.Should().BeHtml("<label></label>");
    }
}
