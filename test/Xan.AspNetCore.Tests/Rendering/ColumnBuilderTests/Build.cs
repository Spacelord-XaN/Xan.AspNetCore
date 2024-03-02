using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderTests;

public class Build
    : TestBase
{
    [Fact]
    public void ShouldReturnDefault()
    {
        //  Arrange        

        //  Act
        ColumnConfig<int> result = Sut.Build();

        //  Assert
        result.Should().BeEquivalentTo(new ColumnConfig<int>());
    }
}
