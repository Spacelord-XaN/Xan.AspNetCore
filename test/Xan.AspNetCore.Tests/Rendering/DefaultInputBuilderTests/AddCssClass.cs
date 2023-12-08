using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultInputBuilderTests;

public class AddCssClass
{
    [Theory]
    [AutoData]
    public void ShouldAddCssClass(string cssClass)
    {
        // Arrange
        TagBuilder input = new ("input");
        DefaultInputBuilder sut = new (input);

        // Act
        sut.AddCssClass(cssClass);

        // Assert
        Assert.Equal(cssClass, input.Attributes["class"]);
    }
}
