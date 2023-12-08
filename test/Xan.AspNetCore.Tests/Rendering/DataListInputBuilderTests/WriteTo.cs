using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.InputBuilderTests;

public class WriteTo
{
    [Theory]
    [AutoData]
    public void ContentAndInput_ShouldWriteContentToWriter(string text, string tagName)
    {
        // Arrange
        HtmlString dataList = new(text);
        TagBuilder input = new(tagName);
        DefaultInputBuilder inputBuilder = new(input);
        DataListInputBuilder sut = new (inputBuilder, dataList);
        using StringWriter writer = new ();

        // Act
        sut.WriteTo(writer, HtmlEncoder.Default);

        // Assert
        writer.ToString().Should().Be(text);
    }
}
