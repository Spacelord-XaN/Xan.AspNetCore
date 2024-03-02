using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnAlignExtensionsTests;

public class ToHtmlString
{
    [Theory]
    [InlineData(ColumnAlign.Center, "center")]
    [InlineData(ColumnAlign.Left, "left")]
    [InlineData(ColumnAlign.Right, "right")]
    public void ShouldReturnCorrectValue(ColumnAlign value, string expectedString)
    {
        //  Arrange

        //  Act
        string result = value.ToHtmlString();

        //  Assert
        Assert.Equal(expectedString, result);
    }
}
