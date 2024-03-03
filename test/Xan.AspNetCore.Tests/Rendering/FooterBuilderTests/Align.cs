using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.FooterBuilderTests;

public class Align
    : TestBase
{
    [Theory]
    [AutoData]
    public void ShouldSetAlign(ColumnAlign align)
    {
        //  Arrange

        //  Act
        ColumnFooterConfig result = Sut.Align(align).Build();

        //  Assert
        result.Align.Should().Be(align);
    }
}
