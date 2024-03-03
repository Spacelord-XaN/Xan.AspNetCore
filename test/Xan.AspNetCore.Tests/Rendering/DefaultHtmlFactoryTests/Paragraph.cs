using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Paragraph
     : TestBase
{
    [Fact]
    public void ShouldReturnCorrectHtml()
    {
        //  Arrange

        //  Act
        TagBuilder result = Sut.Paragraph();

        //  Assert
        result.Should().BeHtml("""<p></p>""");
    }
}
