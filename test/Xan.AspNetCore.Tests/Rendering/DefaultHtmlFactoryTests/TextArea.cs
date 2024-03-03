using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class TextArea
    : TestBase
{
    [Theory]
    [AutoData]
    public void NullValue_AutoFocus(string name)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.TextArea(name, null, true);

        //  Assert
        result.Should().BeHtml($"""<textarea autofocus="" id="id_{name}" name="{name}"></textarea>""");
    }

    [Theory]
    [AutoData]
    public void NullValue_NoAutoFocus(string name)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.TextArea(name, null, false);

        //  Assert
        result.Should().BeHtml($"""<textarea id="id_{name}" name="{name}"></textarea>""");
    }

    [Theory]
    [AutoData]
    public void Value_AutoFocus(string name, string value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.TextArea(name, value, true);

        //  Assert
        result.Should().BeHtml($"""<textarea autofocus="" id="id_{name}" name="{name}">{value}</textarea>""");
    }

    [Theory]
    [AutoData]
    public void Value_NoAutoFocus(string name, string value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.TextArea(name, value, false);

        //  Assert
        result.Should().BeHtml($"""<textarea id="id_{name}" name="{name}">{value}</textarea>""");
    }
}
