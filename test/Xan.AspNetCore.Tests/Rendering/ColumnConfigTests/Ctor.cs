using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnConfigTests;

public class Ctor
{
    [Fact]
    public void ShouldSetDefaultValues()
    {
        // Arrange

        // Act
        ColumnConfig<object> column = new ();

        // Assert
        using (new AssertionScope())
        {
            column.HasFooter.Should().BeFalse();
            column.BreakText.Should().BeFalse();
            column.Align.Should().Be(ColumnAlign.Left);
            column.Footer.Should().BeNull();
            column.Width.Should().Be(Width.Auto);
            column.Title.Should().BeHtml(string.Empty);
        }
    }
}
