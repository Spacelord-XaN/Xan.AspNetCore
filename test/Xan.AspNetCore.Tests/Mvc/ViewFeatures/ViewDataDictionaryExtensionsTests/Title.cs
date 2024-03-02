using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Mvc.ViewFeatures;

namespace Xan.AspNetCore.Tests.Mvc.ViewFeatures.ViewDataDictionaryExtensionsTests;

public class Title
{
    [Fact]
    public void NoTitleSet_ShouldThrow()
    {
        //  Arrange
        Dictionary<string, object?> viewData = [];

        //  Act
        Action act = () => viewData.Title();

        //  Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [AutoData]
    public void TitlIsSet_ShouldReturnTitle(string name, string value)
    {
        //  Arrange
        LocalizedString title = new(name, value);
        Dictionary<string, object?> viewData = new()
        {
            { "Title", title }
        };

        //  Act
        LocalizedString result =  viewData.Title();

        //  Assert
        result.Should().Be(title);
    }
}
