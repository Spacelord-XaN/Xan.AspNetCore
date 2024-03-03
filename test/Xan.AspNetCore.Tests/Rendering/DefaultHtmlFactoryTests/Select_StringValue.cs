using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Select_StringValue
     : TestBase
{
    [Fact]
    public void NoItems()
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.Select("MyName", "MyValue", new SelectList(Enumerable.Empty<string>()));

        //  Assert
        result.Should().BeHtml("""<select id="id_MyName" name="MyName"></select>""");
    }

    [Fact]
    public void NoItems_SubmitOnChange()
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.Select("MyName", "MyValue", new SelectList(Enumerable.Empty<string>()), submitOnChange: true);

        //  Assert
        result.Should().BeHtml("""<select id="id_MyName" name="MyName" onchange="this.form.submit()"></select>""");
    }

    [Fact]
    public void NoItems_AutoFocus()
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.Select("MyName", "MyValue", new SelectList(Enumerable.Empty<string>()), autoFocus: true);

        //  Assert
        result.Should().BeHtml("""<select autofocus="" id="id_MyName" name="MyName"></select>""");
    }

    [Fact]
    public void NoItems_ValueIsNull()
    {
        //  Arrange
        string? value = null;

        //  Act
        IInputBuilder result = Sut.Select("MyName", value, new SelectList(Enumerable.Empty<string>()));

        //  Assert
        result.Should().BeHtml("""<select id="id_MyName" name="MyName"></select>""");
    }

    [Fact]
    public void WithItems()
    {
        //  Arrange
        SelectList items = new (new[] { "Item1", "Item2" });

        //  Act
        IInputBuilder result = Sut.Select("MyName", "MyValue", items);

        //  Assert
        result.Should().BeHtml("""<select id="id_MyName" name="MyName"><option value="null">Item1</option><option value="null">Item2</option></select>""");
    }

    [Fact]
    public void WithAll()
    {
        //  Arrange
        SelectList items = new(new[] { "Item1", "Item2" });

        //  Act
        IInputBuilder result = Sut.Select("MyName", "MyValue", items, submitOnChange: true, autoFocus: true);

        //  Assert
        result.Should().BeHtml("""<select autofocus="" id="id_MyName" name="MyName" onchange="this.form.submit()"><option value="null">Item1</option><option value="null">Item2</option></select>""");
    }
}
