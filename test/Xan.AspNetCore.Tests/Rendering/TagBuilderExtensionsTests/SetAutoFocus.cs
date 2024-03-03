using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.TagBuilderExtensionsTests;

public class SetAutoFocus
{
    [Fact]
    public void ShouldSetAutoFocusAttribute()
    {
        // Arrange
        TagBuilder sut = new ("input");

        // Act
        sut.SetAutoFocus();

        // Assert
        sut.Should().BeHtml("""<input autofocus=""></input>""");
    }
}
