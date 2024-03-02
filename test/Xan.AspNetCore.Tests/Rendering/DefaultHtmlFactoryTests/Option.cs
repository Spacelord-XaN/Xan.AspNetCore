using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Option
{
    [Fact]
    public void Value_ShouldReturnHtml()
    {
        //  Arrange
        DefaultHtmlFactory sut = new(new StringLocalizerMock());

        //  Act
        TagBuilder option = sut.Option("ThisIsTheValue");

        //  Assert
        option.Should().BeHtml("""<option value="ThisIsTheValue"></option>""");
    }

    [Fact]
    public void NullValue_ShouldReturnHtml()
    {
        //  Arrange
        DefaultHtmlFactory sut = new(new StringLocalizerMock());

        //  Act
        TagBuilder option = sut.Option(null);

        //  Assert
        option.Should().BeHtml("""<option value="null"></option>""");
    }
}
