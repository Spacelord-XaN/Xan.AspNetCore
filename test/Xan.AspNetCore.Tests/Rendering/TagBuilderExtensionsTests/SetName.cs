using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.TagBuilderExtensionsTests;

public class SetName
{
    [Theory]
    [AutoData]
    public void ShouldSetNameAttribute(string name)
    {
        // Arrange
        TagBuilder sut = new ("input");

        // Act
        sut.SetName(name);

        // Assert
        sut.Should().BeHtml($"""<input name="{name}"></input>""");
    }
}
