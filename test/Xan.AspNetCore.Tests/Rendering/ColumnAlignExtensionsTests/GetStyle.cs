using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnAlignExtensionsTests;

public class GetStyle
{
    [Theory]
    [InlineData(ColumnAlign.Center, "text-align: center;")]
    [InlineData(ColumnAlign.Left, "text-align: left;")]
    [InlineData(ColumnAlign.Right, "text-align: right;")]
    public void ShouldReturnCorrectValue(ColumnAlign value, string expectedString)
    {
        //  Arrange

        //  Act
        string result = value.GetStyle();

        //  Assert
        Assert.Equal(expectedString, result);
    }
}
