using AutoFixture.Xunit2;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultInputBuilderTests;

public class Attributes
{
    [Theory]
    [AutoData]
    public void ShouldSetAttributesFromInput(string key, string value)
    {
        //  Arrange
        TagBuilder input = new("input");
        DefaultInputBuilder sut = new(input);

        //  Act
        sut.Attributes.Add(key, value);

        //  Assert
        using (new AssertionScope())
        {
            input.Attributes.Should().ContainKey(key);
            input.Attributes[key].Should().Be(value);
        }
    }
}
