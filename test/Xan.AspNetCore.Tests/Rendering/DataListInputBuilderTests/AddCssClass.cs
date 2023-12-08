using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DataListInputBuilderTests;

public class AddCssClass
{
    [Theory]
    [AutoData]
    public void ShouldAddCssClass(string cssClass, string text, string tagName)
    {
        // Arrange
        HtmlString dataList = new(text);
        TagBuilder input = new(tagName);
        DefaultInputBuilder inputBuilder = new(input);

        DataListInputBuilder sut = new (inputBuilder, dataList);

        // Act
        sut.AddCssClass(cssClass);

        // Assert
        Assert.Equal(cssClass, input.Attributes["class"]);
    }
}
