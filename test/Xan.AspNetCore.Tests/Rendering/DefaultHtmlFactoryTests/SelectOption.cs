using Microsoft.AspNetCore.Html;

namespace Xan.AspNetCore.Tests.Rendering.DefaultBoostrapHtmlFactoryTests;

public class SelectOption
     : TestBase
{
    [Theory]
    [AutoData]
    public void NotSelected(string text, string value)
    {
        //  Arrange

        //  Act
        IHtmlContent result = Sut.SelectOption(text, value, false);

        //  Assert
        result.Should().BeHtml($"""<option value="{value}">{text}</option>""");
    }

    [Theory]
    [AutoData]
    public void Selected(string text, string value)
    {
        //  Arrange

        //  Act
        IHtmlContent result = Sut.SelectOption(text, value, true);

        //  Assert
        result.Should().BeHtml($"""<option selected="" value="{value}">{text}</option>""");
    }

    [Theory]
    [AutoData]
    public void NotSelected_ValueNull(string text)
    {
        //  Arrange

        //  Act
        IHtmlContent result = Sut.SelectOption(text, null, false);

        //  Assert
        result.Should().BeHtml($"""<option value="null">{text}</option>""");
    }

    [Theory]
    [AutoData]
    public void Selected_ValueNull(string text)
    {
        //  Arrange

        //  Act
        IHtmlContent result = Sut.SelectOption(text, null, true);

        //  Assert
        result.Should().BeHtml($"""<option selected="" value="null">{text}</option>""");
    }
}
