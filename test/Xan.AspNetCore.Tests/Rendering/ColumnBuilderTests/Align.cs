using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderTests;

public class Align
    : TestBase
{
    [Theory]
    [AutoData]
    public void ShouldSetAlign(ColumnAlign align)
    {
        //  Arrange        

        //  Act
        ColumnConfig<int> result = Sut.Align(align).Build();

        //  Assert
        result.Align.Should().Be(align);
    }
}
