using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderTests;

public class BreakText
    : TestBase
{
    [Fact]
    public void ShouldSetBreakText()
    {
        //  Arrange        

        //  Act
        ColumnConfig<int> result = Sut.BreakText().Build();

        //  Assert
        result.BreakText.Should().BeTrue();
    }
}
