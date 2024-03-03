using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Input
    : TestBase
{
    [Theory]
    [AutoData]
    public void ValueIsNull_NoAutoFocus(string type, string name)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.Input(type, name, null, false);

        //  Assert
        result.Should().BeHtml($"""<input id="id_{name}" name="{name}" type="{type}"></input>""");
    }
    
    [Theory]
    [AutoData]
    public void ValueIsNull_AutoFocus(string type, string name)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.Input(type, name, null, true);

        //  Assert
        result.Should().BeHtml($"""<input autofocus="" id="id_{name}" name="{name}" type="{type}"></input>""");
    }

    [Theory]
    [AutoData]
    public void ValueIsNotNull_NoAutoFocus(string type, string name, string value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.Input(type, name, value, false);

        //  Assert
        result.Should().BeHtml($"""<input id="id_{name}" name="{name}" type="{type}" value="{value}"></input>""");
    }

    [Theory]
    [AutoData]
    public void ValueIsNotNull_AutoFocus(string type, string name, string value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.Input(type, name, value, true);

        //  Assert
        result.Should().BeHtml($"""<input autofocus="" id="id_{name}" name="{name}" type="{type}" value="{value}"></input>""");
    }
}
