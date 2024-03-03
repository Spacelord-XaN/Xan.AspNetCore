using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Table_Generic
     : TestBase
{
    [Theory]
    [AutoData]
    public void ShouldReturnCorrectHtml(int[] items)
    {
        //  Arrange

        //  Act
        TableBuilder<int> result = Sut.Table(items);

        //  Assert
        using (new AssertionScope())
        {
            result.Count.Should().Be(0);
            result.Html.Should().Be(Sut);
            result.Localizer.Should().Be(Sut.Localizer);
            
            result.Build().Should().BeHtml("""<table><thead><tr></tr></thead><tbody><tr></tr><tr></tr><tr></tr></tbody></table>""");
        }
    }
}
