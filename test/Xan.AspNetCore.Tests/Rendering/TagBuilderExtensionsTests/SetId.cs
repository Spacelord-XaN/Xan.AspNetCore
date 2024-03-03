using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.TagBuilderExtensionsTests;

public class SetId
{
    [Theory]
    [AutoData]
    public void ShouldSetIdAttribute(string id)
    {
        // Arrange
        TagBuilder sut = new ("input");

        // Act
        sut.SetId(id);

        // Assert
        sut.Should().BeHtml($"""<input id="{id}"></input>""");
    }
}
