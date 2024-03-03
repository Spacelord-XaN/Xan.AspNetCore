using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Option
     : TestBase
{
    [Fact]
    public void Value_ShouldReturnHtml()
    {
        //  Arrange

        //  Act
        TagBuilder option = Sut.Option("ThisIsTheValue");

        //  Assert
        option.Should().BeHtml("""<option value="ThisIsTheValue"></option>""");
    }

    [Fact]
    public void NullValue_ShouldReturnHtml()
    {
        //  Arrange

        //  Act
        TagBuilder option = Sut.Option(null);

        //  Assert
        option.Should().BeHtml("""<option value="null"></option>""");
    }
}
