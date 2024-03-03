using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Select_EnumValue
     : TestBase
{
    public enum MyEnum
    {
        Item1,
        Item2
    }

    [Theory]
    [AutoData]
    public void NoItems(string name, MyEnum value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.Select(name, value, new SelectList(Enumerable.Empty<string>()));

        //  Assert
        result.Should().BeHtml($"""<select id="id_{name}" name="{name}"></select>""");
    }

    [Theory]
    [AutoData]
    public void NoItems_SubmitOnChange(string name, MyEnum value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.Select(name, value, new SelectList(Enumerable.Empty<string>()), submitOnChange: true);

        //  Assert
        result.Should().BeHtml($"""<select id="id_{name}" name="{name}" onchange="this.form.submit()"></select>""");
    }

    [Theory]
    [AutoData]
    public void NoItems_AutoFocus(string name, MyEnum value)
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.Select(name, value, new SelectList(Enumerable.Empty<string>()), autoFocus: true);

        //  Assert
        result.Should().BeHtml($"""<select autofocus="" id="id_{name}" name="{name}"></select>""");
    }

    [Theory]
    [AutoData]
    public void NoItems_ValueIsNull(string name)
    {
        //  Arrange
        MyEnum? value = null;

        //  Act
        IInputBuilder result = Sut.Select(name, value, new SelectList(Enumerable.Empty<string>()));

        //  Assert
        result.Should().BeHtml($"""<select id="id_{name}" name="{name}"></select>""");
    }

    [Theory]
    [AutoData]
    public void WithItems(string name, MyEnum value, string item1, string item2)
    {
        //  Arrange
        SelectList items = new (new[] { item1, item2 });

        //  Act
        IInputBuilder result = Sut.Select(name, value, items);

        //  Assert
        result.Should().BeHtml($"""<select id="id_{name}" name="{name}"><option value="null">{item1}</option><option value="null">{item2}</option></select>""");
    }

    [Theory]
    [AutoData]
    public void WithAll(string name, MyEnum value, string item1, string item2)
    {
        //  Arrange
        SelectList items = new(new[] { item1, item2 });

        //  Act
        IInputBuilder result = Sut.Select(name, value, items, submitOnChange: true, autoFocus: true);

        //  Assert
        result.Should().BeHtml($"""<select autofocus="" id="id_{name}" name="{name}" onchange="this.form.submit()"><option value="null">{item1}</option><option value="null">{item2}</option></select>""");
    }
}
