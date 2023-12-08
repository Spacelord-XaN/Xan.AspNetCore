using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultBoostrapHtmlFactoryTests;

public class Select_StringValue
{
    [Fact]
    public void NoItems()
    {
        //  Arrange
        DefaultBoostrapHtmlFactory factory = new(Mockups.StringLocalizer());

        //  Act
        IInputBuilder result = factory.Select("MyName", "MyValue", new SelectList(Enumerable.Empty<string>()));

        //  Assert
        result.Should().Html("""<select class="form-select" id="id_MyName" name="MyName"></select>""");
    }

    [Fact]
    public void NoItems_SubmitOnChange()
    {
        //  Arrange
        DefaultBoostrapHtmlFactory factory = new(Mockups.StringLocalizer());

        //  Act
        IInputBuilder result = factory.Select("MyName", "MyValue", new SelectList(Enumerable.Empty<string>()), submitOnChange: true);

        //  Assert
        result.Should().Html("""<select class="form-select" id="id_MyName" name="MyName" onchange="this.form.submit()"></select>""");
    }

    [Fact]
    public void NoItems_AutoFocus()
    {
        //  Arrange
        DefaultBoostrapHtmlFactory factory = new(Mockups.StringLocalizer());

        //  Act
        IInputBuilder result = factory.Select("MyName", "MyValue", new SelectList(Enumerable.Empty<string>()), autoFocus: true);

        //  Assert
        result.Should().Html("""<select autofocus="" class="form-select" id="id_MyName" name="MyName"></select>""");
    }

    [Fact]
    public void NoItems_ValueIsNull()
    {
        //  Arrange
        DefaultBoostrapHtmlFactory factory = new(Mockups.StringLocalizer());
        string? value = null;

        //  Act
        IInputBuilder result = factory.Select("MyName", value, new SelectList(Enumerable.Empty<string>()));

        //  Assert
        result.Should().Html("""<select class="form-select" id="id_MyName" name="MyName"></select>""");
    }

    [Fact]
    public void WithItems()
    {
        //  Arrange
        DefaultBoostrapHtmlFactory factory = new(Mockups.StringLocalizer());
        SelectList items = new (new[] { "Item1", "Item2" });

        //  Act
        IInputBuilder result = factory.Select("MyName", "MyValue", items);

        //  Assert
        result.Should().Html("""<select class="form-select" id="id_MyName" name="MyName"><option value="null">Item1</option><option value="null">Item2</option></select>""");
    }

    [Fact]
    public void WithAll()
    {
        //  Arrange
        DefaultBoostrapHtmlFactory factory = new(Mockups.StringLocalizer());
        SelectList items = new(new[] { "Item1", "Item2" });

        //  Act
        IInputBuilder result = factory.Select("MyName", "MyValue", items, submitOnChange: true, autoFocus: true);

        //  Assert
        result.Should().Html("""<select autofocus="" class="form-select" id="id_MyName" name="MyName" onchange="this.form.submit()"><option value="null">Item1</option><option value="null">Item2</option></select>""");
    }
}
