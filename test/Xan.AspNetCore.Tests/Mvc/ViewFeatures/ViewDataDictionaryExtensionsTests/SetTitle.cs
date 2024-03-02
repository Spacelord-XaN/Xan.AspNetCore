using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Mvc.ViewFeatures;

namespace Xan.AspNetCore.Tests.Mvc.ViewFeatures.ViewDataDictionaryExtensionsTests;

public class SetTitle
{
    [Theory]
    [AutoData]
    public void ShouldSetTitle(string name, string value)
    {
        //  Arrange
        Dictionary<string, object?> viewData = [];
        LocalizedString title = new(name, value);

        //  Act
        viewData.SetTitle(title);

        //  Assert
        viewData.Should().ContainKey("Title");
        viewData["Title"].Should().Be(title);
    }
}
