using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Th
     : TestBase
{
    [Theory]
    [InlineData(TableScope.None, "<th></th>")]
    [InlineData(TableScope.Col, "<th scope=\"col\"></th>")]
    public void ShouldReturnCorrectHtml(TableScope scope, string expectedHtml)
    {
        //  Arrange

        //  Act
        TagBuilder result = Sut.Th(scope);

        //  Assert
        result.Should().BeHtml(expectedHtml);
    }
}
