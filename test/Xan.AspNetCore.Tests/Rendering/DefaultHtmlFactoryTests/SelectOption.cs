using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class SelectOption
{
    [Fact]
    public void ValueAndSelected_ReturnsHtml()
    {
        //  Arrange
        DefaultHtmlFactory sut = new(new StringLocalizerMock());

        //  Act
        TagBuilder option = sut.SelectOption("ThisIsTheText", "ThisIsTheValue", true);

        //  Assert
        option.Should().Html("""<option selected="" value="ThisIsTheValue">ThisIsTheText</option>""");
    }

    [Fact]
    public void ValueAndNotSelected_ReturnsHtml()
    {
        //  Arrange
        DefaultHtmlFactory sut = new(new StringLocalizerMock());

        //  Act
        TagBuilder option = sut.SelectOption("ThisIsTheText", "ThisIsTheValue", false);

        //  Assert
        option.Should().Html("""<option value="ThisIsTheValue">ThisIsTheText</option>""");
    }

    [Fact]
    public void NullValueAndNotSelected_ReturnsHtml()
    {
        //  Arrange
        DefaultHtmlFactory sut = new(new StringLocalizerMock());

        //  Act
        TagBuilder option = sut.SelectOption("ThisIsTheText", null, false);

        //  Assert
        option.Should().Html("""<option value="null">ThisIsTheText</option>""");
    }

    [Fact]
    public void NullValueAndSelected_ReturnsHtml()
    {
        //  Arrange
        DefaultHtmlFactory sut = new(new StringLocalizerMock());

        //  Act
        TagBuilder option = sut.SelectOption("ThisIsTheText", null, true);

        //  Assert
        option.Should().Html("""<option selected="" value="null">ThisIsTheText</option>""");
    }
}
