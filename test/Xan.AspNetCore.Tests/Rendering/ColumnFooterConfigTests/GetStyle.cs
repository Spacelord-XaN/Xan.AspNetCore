using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnFooterConfigTests;

public class GetStyle
{
    [Theory]
    [AutoData]
    public void ShouldReturnOnlyAlignStyle(ColumnAlign align)
    {
        //  Arrange
        ColumnFooterConfig sut = new()
        {
            Align = align
        };

        //  Act
        string result = sut.GetStyle();

        //  Assert
        result.Should().Be(align.GetStyle());
    }
}
