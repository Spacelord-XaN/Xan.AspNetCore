using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.TableScopeExtensions;

public class ToHtmlAttributeString
{
    [Theory]
    [InlineData(TableScope.None, "")]
    [InlineData(TableScope.Col, "col")]
    public void ShouldReturnCorrectValue(TableScope value, string expectedString)
    {
        //  Arrange

        //  Act
        string result = value.ToHtmlAttributeString();

        //  Assert
        Assert.Equal(expectedString, result);
    }
}
