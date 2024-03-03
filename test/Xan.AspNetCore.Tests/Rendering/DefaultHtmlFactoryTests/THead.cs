using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class THead
     : TestBase
{
    [Fact]
    public void ShouldReturnCorrectHtml()
    {
        //  Arrange

        //  Act
        TagBuilder result = Sut.THead();

        //  Assert
        result.Should().BeHtml("""<thead></thead>""");
    }
}
