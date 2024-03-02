using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class DataList
{
    [Fact]
    public void NoItems()
    {
        //  Arrange
        DefaultHtmlFactory factory = new(new StringLocalizerMock());

        //  Act
        IInputBuilder result = factory.DataList("MyName", "MyValue", new HashSet<string?>());

        //  Assert
        result.Should().BeHtml("""<input id="id_MyName" list="id_MyNameOptions" name="MyName" type="text" value="MyValue"></input><datalist id="id_MyNameOptions" name="MyNameOptions"></datalist>""");
    }

    [Fact]
    public void NoItems_AutoFocus()
    {
        //  Arrange
        DefaultHtmlFactory factory = new(new StringLocalizerMock());

        //  Act
        IInputBuilder result = factory.DataList("MyName", "MyValue", new HashSet<string?>(), autoFocus: true);

        //  Assert
        result.Should().BeHtml("""<input autofocus="" id="id_MyName" list="id_MyNameOptions" name="MyName" type="text" value="MyValue"></input><datalist id="id_MyNameOptions" name="MyNameOptions"></datalist>""");
    }

    [Fact]
    public void NoItems_ValueIsNull()
    {
        //  Arrange
        DefaultHtmlFactory factory = new(new StringLocalizerMock());
        string? value = null;

        //  Act
        IInputBuilder result = factory.DataList("MyName", value, new HashSet<string?>());

        //  Assert
        result.Should().BeHtml("""<input id="id_MyName" list="id_MyNameOptions" name="MyName" type="text"></input><datalist id="id_MyNameOptions" name="MyNameOptions"></datalist>""");
    }

    [Fact]
    public void WithItems()
    {
        //  Arrange
        DefaultHtmlFactory factory = new(new StringLocalizerMock());
        HashSet<string?> items = new () { "Item1", "Item2" };

        //  Act
        IInputBuilder result = factory.DataList("MyName", "MyValue", items);

        //  Assert
        result.Should().BeHtml("""<input id="id_MyName" list="id_MyNameOptions" name="MyName" type="text" value="MyValue"></input><datalist id="id_MyNameOptions" name="MyNameOptions"><option value="Item1"></option><option value="Item2"></option></datalist>""");
    }

    [Fact]
    public void WithAll()
    {
        //  Arrange
        DefaultHtmlFactory factory = new(new StringLocalizerMock());
        HashSet<string?> items = new() { "Item1", "Item2" };

        //  Act
        IInputBuilder result = factory.DataList("MyName", "MyValue", items, autoFocus: true);

        //  Assert
        result.Should().BeHtml("""<input autofocus="" id="id_MyName" list="id_MyNameOptions" name="MyName" type="text" value="MyValue"></input><datalist id="id_MyNameOptions" name="MyNameOptions"><option value="Item1"></option><option value="Item2"></option></datalist>""");
    }
}
