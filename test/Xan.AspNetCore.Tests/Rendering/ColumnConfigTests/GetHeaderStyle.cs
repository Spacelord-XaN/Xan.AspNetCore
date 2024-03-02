using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnConfigTests;

public class GetHeaderStyle
{
    [Fact]
    public void WithAlign()
    {
        //  Arrange
        ColumnConfig<object> sut = new()
        {
            Align = ColumnAlign.Right
        };

        //  Act
        string result = sut.GetHeaderStyle();

        //  Assert
        result.Should().Be("width: auto;text-align: right;white-space: nowrap;");
    }

    [Fact]
    public void WithWidth()
    {
        //  Arrange
        ColumnConfig<object> sut = new()
        {
            Width = Width.Percent(200)
        };

        //  Act
        string result = sut.GetHeaderStyle();

        //  Assert
        result.Should().Be("width: 200%;text-align: left;white-space: nowrap;");
    }

    [Fact]
    public void WithAlignAndWidth()
    {
        //  Arrange
        ColumnConfig<object> sut = new()
        {
            Align = ColumnAlign.Center,
            Width = Width.Percent(40)
        };

        //  Act
        string result = sut.GetHeaderStyle();

        //  Assert
        result.Should().Be("width: 40%;text-align: center;white-space: nowrap;");
    }
}
