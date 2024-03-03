using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class TextInput
    : TestBase
{
    [Theory]
    [AutoData]
    public void NullValue_AutoFocus(string name)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.TextInput(name, null, true);

        //  Assert
        result.Should().BeHtml($"""<input autofocus="" id="id_{name}" name="{name}" type="text"></input>""");
    }

    [Theory]
    [AutoData]
    public void NullValue_NoAutoFocus(string name)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.TextInput(name, null, false);

        //  Assert
        result.Should().BeHtml($"""<input id="id_{name}" name="{name}" type="text"></input>""");
    }

    [Theory]
    [AutoData]
    public void Value_AutoFocus(string name, string value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.TextInput(name, value, true);

        //  Assert
        result.Should().BeHtml($"""<input autofocus="" id="id_{name}" name="{name}" type="text" value="{value}"></input>""");
    }

    [Theory]
    [AutoData]
    public void Value_NoAutoFocus(string name, string value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.TextInput(name, value, false);

        //  Assert
        result.Should().BeHtml($"""<input id="id_{name}" name="{name}" type="text" value="{value}"></input>""");
    }
}
