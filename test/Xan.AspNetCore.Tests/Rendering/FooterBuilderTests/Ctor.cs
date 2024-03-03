using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.FooterBuilderTests;

public class Ctor
{
    [Fact]
    public void ShouldInitDefaults()
    {
        //  Arrange
        IHtmlFactory html = X.StrictFake<IHtmlFactory>();
        IStringLocalizer localizer = X.StrictFake<IStringLocalizer>();

        //  Act
        FooterBuilder sut = new(html, localizer);

        //  Assert
        using (new AssertionScope())
        {
            sut.Localizer.Should().BeSameAs(localizer);
            sut.Html.Should().BeSameAs(html);
            ColumnFooterConfig config = sut.Build();
            config.Align.Should().Be(ColumnAlign.Left);
            config.Content.Should().BeHtml("");
        }
    }
}
