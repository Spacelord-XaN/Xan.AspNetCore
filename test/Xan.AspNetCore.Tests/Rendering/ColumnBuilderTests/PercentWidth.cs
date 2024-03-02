using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderTests;

public class PercentWidth
    : TestBase
{
    [Theory]
    [AutoData]
    public void ShouldSetWidthToPercen(int width)
    {
        //  Arrange        

        //  Act
        ColumnConfig<int> result = Sut.PercentWidth(width).Build();

        //  Assert
        result.Width.Should().Be(Width.Percent(width));
    }
}
