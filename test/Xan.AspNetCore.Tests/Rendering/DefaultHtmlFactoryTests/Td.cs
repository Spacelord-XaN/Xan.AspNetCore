using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Td
     : TestBase
{
    [Theory]
    [InlineData(TableScope.None, "<td></td>")]
    [InlineData(TableScope.Col, "<td scope=\"col\"></td>")]
    public void ShouldReturnCorrectHtml(TableScope scope, string expectedHtml)
    {
        //  Arrange

        //  Act
        TagBuilder result = Sut.Td(scope);

        //  Assert
        result.Should().BeHtml(expectedHtml);
    }
}
