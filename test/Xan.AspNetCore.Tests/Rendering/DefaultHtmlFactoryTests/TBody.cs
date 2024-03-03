using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class TBody
     : TestBase
{
    [Fact]
    public void ShouldReturnCorrectHtml()
    {
        //  Arrange

        //  Act
        TagBuilder result = Sut.TBody();

        //  Assert
        result.Should().BeHtml("""<tbody></tbody>""");
    }
}
