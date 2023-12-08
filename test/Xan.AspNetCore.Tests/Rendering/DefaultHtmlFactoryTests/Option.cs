using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Option
{
    [Fact]
    public void Value_ShouldReturnHtml()
    {
        //  Arrange
        DefaultHtmlFactory sut = new(Mockups.StringLocalizer());

        //  Act
        TagBuilder option = sut.Option("ThisIsTheValue");

        //  Assert
        option.Should().Html("""<option value="ThisIsTheValue"></option>""");
    }

    [Fact]
    public void NullValue_ShouldReturnHtml()
    {
        //  Arrange
        DefaultHtmlFactory sut = new(Mockups.StringLocalizer());

        //  Act
        TagBuilder option = sut.Option(null);

        //  Assert
        option.Should().Html("""<option value="null"></option>""");
    }
}
