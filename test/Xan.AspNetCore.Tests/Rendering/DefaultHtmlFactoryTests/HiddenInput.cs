using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class HiddenInput
    : TestBase
{
    [Theory]
    [AutoData]
    public void Int(string name, int value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.HiddenInput(name, value);

        //  Assert
        result.Should().BeHtml($"""<input id="id_{name}" name="{name}" type="hidden" value="{value}"></input>""");
    }

    [Theory]
    [AutoData]
    public void String(string name, string value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.HiddenInput(name, value);

        //  Assert
        result.Should().BeHtml($"""<input id="id_{name}" name="{name}" type="hidden" value="{value}"></input>""");
    }

    [Theory]
    [AutoData]
    public void DateTime(string name)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.HiddenInput(name, new DateTime(2063, 04, 05, 11, 22, 33));

        //  Assert
        result.Should().BeHtml($"""<input id="id_{name}" name="{name}" type="hidden" value="2063-04-05T11:22"></input>""");
    }
}
