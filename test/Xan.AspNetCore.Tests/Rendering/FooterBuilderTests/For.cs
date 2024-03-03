using Microsoft.AspNetCore.Html;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.FooterBuilderTests;

public class For
    : TestBase
{
    [Fact]
    public void ShouldSetContent()
    {
        //  Arrange
        IHtmlContent content = X.StrictFake<IHtmlContent>();

        //  Act
        ColumnFooterConfig result = Sut.For(content).Build();

        //  Assert
        result.Content.Should().BeSameAs(content);
    }
}
