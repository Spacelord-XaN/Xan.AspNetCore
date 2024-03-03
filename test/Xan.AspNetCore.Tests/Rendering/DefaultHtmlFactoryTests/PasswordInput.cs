using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class PasswordInput
    : TestBase
{
    [Theory]
    [AutoData]
    public void NullValue_AutoFocus(string name)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.PasswordInput(name, null, true);

        //  Assert
        result.Should().BeHtml($"""<input autofocus="" id="id_{name}" name="{name}" type="password"></input>""");
    }

    [Theory]
    [AutoData]
    public void NullValue_NoAutoFocus(string name)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.PasswordInput(name, null, false);

        //  Assert
        result.Should().BeHtml($"""<input id="id_{name}" name="{name}" type="password"></input>""");
    }

    [Theory]
    [AutoData]
    public void Value_AutoFocus(string name, string value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.PasswordInput(name, value, true);

        //  Assert
        result.Should().BeHtml($"""<input autofocus="" id="id_{name}" name="{name}" type="password" value="{value}"></input>""");
    }

    [Theory]
    [AutoData]
    public void Value_NoAutoFocus(string name, string value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.PasswordInput(name, value, false);

        //  Assert
        result.Should().BeHtml($"""<input id="id_{name}" name="{name}" type="password" value="{value}"></input>""");
    }
}
