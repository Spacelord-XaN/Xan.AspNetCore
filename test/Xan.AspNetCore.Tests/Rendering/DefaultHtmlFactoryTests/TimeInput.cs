using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class TimeInput
    : TestBase
{
    [Theory]
    [AutoData]
    public void AutoFocus(string name)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.TimeInput(name, new TimeOnly(11, 22, 33, 444), true);

        //  Assert
        result.Should().BeHtml($"""<input autofocus="" id="id_{name}" name="{name}" type="time" value="11:22"></input>""");
    }

    [Theory]
    [AutoData]
    public void NoAutoFocus(string name)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.TimeInput(name, new TimeOnly(11, 22, 33, 444), false);

        //  Assert
        result.Should().BeHtml($"""<input id="id_{name}" name="{name}" type="time" value="11:22"></input>""");
    }
}
