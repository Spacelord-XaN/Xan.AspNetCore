using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class CheckBox
    : TestBase
{
    [Theory]
    [AutoData]
    public void True(string name)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.CheckBox(name, true);

        //  Assert
        result.Should().BeHtml($"""<input checked="" id="id_{name}" name="{name}" type="checkbox" value="true"></input>""");
    }

    [Theory]
    [AutoData]
    public void False(string name)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.CheckBox(name, false);

        //  Assert
        result.Should().BeHtml($"""<input id="id_{name}" name="{name}" type="checkbox" value="true"></input>""");
    }
}
