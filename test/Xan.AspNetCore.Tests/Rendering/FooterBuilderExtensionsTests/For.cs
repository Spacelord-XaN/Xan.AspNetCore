using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.FooterBuilderExtensionsTests;

public class For
    : TestBase
{
    [Theory]
    [AutoData]
    public void ShouldSetContent(string value)
    {
        //  Arrange

        //  Act
        ColumnFooterConfig result = Sut.For(value).Build();

        //  Assert
        result.Content.Should().BeHtml(value);
    }
}
