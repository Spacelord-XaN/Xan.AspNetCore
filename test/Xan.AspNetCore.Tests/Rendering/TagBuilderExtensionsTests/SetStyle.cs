using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.TagBuilderExtensionsTests;

public class SetStyle
{
    [Theory]
    [AutoData]
    public void ShouldSetStyleAttribute(string style)
    {
        // Arrange
        TagBuilder sut = new ("input");

        // Act
        sut.SetStyle(style);

        // Assert
        sut.Should().BeHtml($"""<input style="{style}"></input>""");
    }
}
