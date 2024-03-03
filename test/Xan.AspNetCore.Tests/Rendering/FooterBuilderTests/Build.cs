using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.FooterBuilderTests;

public class Build
    : TestBase
{
    [Fact]
    public void ShouldReturnDefault()
    {
        //  Arrange        

        //  Act
        ColumnFooterConfig result = Sut.Build();

        //  Assert
        result.Should().BeEquivalentTo(new ColumnFooterConfig());
    }
}
