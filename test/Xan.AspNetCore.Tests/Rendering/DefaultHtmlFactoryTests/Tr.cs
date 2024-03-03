using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Tr
     : TestBase
{
    [Fact]
    public void ShouldReturnCorrectHtml()
    {
        //  Arrange

        //  Act
        TagBuilder result = Sut.Tr();

        //  Assert
        result.Should().BeHtml("""<tr></tr>""");
    }
}
