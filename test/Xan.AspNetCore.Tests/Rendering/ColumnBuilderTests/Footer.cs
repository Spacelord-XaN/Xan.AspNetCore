using Microsoft.AspNetCore.Html;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderTests;

public class Footer
    : TestBase
{
    [Theory]
    [AutoData]
    public void ShouldCallConfigureAndAddFooter(ColumnAlign footerAlign, string footerContent)
    {
        //  Arrange

        //  Act
        ColumnConfig<int> result = Sut.Footer(footer =>
        {
            footer.Align(footerAlign).For(new HtmlString(footerContent));
        }).Build();

        //  Assert
        using (new AssertionScope())
        {
            result.Footer.Should().NotBeNull();
            result.Footer!.Align.Should().Be(footerAlign);
            result.Footer!.Content.Should().BeHtml(footerContent);
        }
    }
}
