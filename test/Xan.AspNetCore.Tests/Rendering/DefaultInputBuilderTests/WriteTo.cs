using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultInputBuilderTests;

public class WriteTo
{
    [Fact]
    public void OnlyInput_ShouldWriteInputToWriter()
    {
        // Arrange
        TagBuilder input = new("input");
        using StringWriter writer = new();
        DefaultInputBuilder sut = new(input);

        // Act
        sut.WriteTo(writer, HtmlEncoder.Default);

        // Assert
        writer.ToString().Should().Be("<input></input>");
    }
}
