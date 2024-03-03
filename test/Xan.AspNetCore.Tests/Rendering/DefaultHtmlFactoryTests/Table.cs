using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Table
     : TestBase
{
    [Fact]
    public void ShouldReturnCorrectHtml()
    {
        //  Arrange

        //  Act
        TagBuilder result = Sut.Table();

        //  Assert
        result.Should().BeHtml("""<table></table>""");
    }
}
