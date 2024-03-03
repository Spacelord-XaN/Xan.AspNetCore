using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class NumberInput
    : TestBase
{
    [Theory]
    [AutoData]
    public void Int_AutoFocus(string name, int value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.NumberInput(name, value, true);

        //  Assert
        result.Should().BeHtml($"""<input autofocus="" id="id_{name}" name="{name}" type="text" value="{value}"></input>""");
    }

    [Theory]
    [AutoData]
    public void Int_NoAutoFocus(string name, int value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.NumberInput(name, value, false);

        //  Assert
        result.Should().BeHtml($"""<input id="id_{name}" name="{name}" type="text" value="{value}"></input>""");
    }


    [Theory]
    [AutoData]
    public void Double_AutoFocus(string name, double value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.NumberInput(name, value, true);

        //  Assert
        result.Should().BeHtml($"""<input autofocus="" id="id_{name}" name="{name}" type="text" value="{value}"></input>""");
    }

    [Theory]
    [AutoData]
    public void Double_NoAutoFocus(string name, double value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.NumberInput(name, value, false);

        //  Assert
        result.Should().BeHtml($"""<input id="id_{name}" name="{name}" type="text" value="{value}"></input>""");
    }


    [Theory]
    [AutoData]
    public void Decimal_AutoFocus(string name, decimal value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.NumberInput(name, value, true);

        //  Assert
        result.Should().BeHtml($"""<input autofocus="" id="id_{name}" name="{name}" type="text" value="{value}"></input>""");
    }

    [Theory]
    [AutoData]
    public void Decimal_NoAutoFocus(string name, decimal value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.NumberInput(name, value, false);

        //  Assert
        result.Should().BeHtml($"""<input id="id_{name}" name="{name}" type="text" value="{value}"></input>""");
    }
}
