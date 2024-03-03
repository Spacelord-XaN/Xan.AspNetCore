using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.TagBuilderExtensionsTests;

public class SetScope
{
    [Theory]
    [AutoData]
    public void ShouldSetScopeAttribute(string scope)
    {
        // Arrange
        TagBuilder sut = new ("input");

        // Act
        sut.SetScope(scope);

        // Assert
        sut.Should().BeHtml($"""<input scope="{scope}"></input>""");
    }
}
