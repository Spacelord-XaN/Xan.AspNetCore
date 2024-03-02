using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnFooterConfigTests;

public class Ctor
{
    [Fact]
    public void ShouldInitDefaults()
    {
        //  Arrange

        //  Act
        ColumnFooterConfig sut = new ();

        //  Assert
        using (new AssertionScope())
        {
            sut.Align.Should().Be(ColumnAlign.Left);
            sut.Content.Should().BeHtml("");
        }
    }
}
