using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderTests;

public class AutoWidth
    : TestBase
{
    [Fact]
    public void ShouldSetWidthToAuto()
    {
        //  Arrange        

        //  Act
        ColumnConfig<int> result = Sut.AutoWidth().Build();

        //  Assert
        result.Width.Should().Be(Width.Auto);
    }
}
