using AutoFixture.Xunit2;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.InputBuilderTests;

public class Attributes
{
    [Theory]
    [AutoData]
    public void ContentAndInput_ShouldSetAttributesFromInput(string key, string value, string dataListTagName, string inputTagName)
    {
        //  Arrange
        TagBuilder dataList = new(dataListTagName);
        TagBuilder input = new(inputTagName);
        DefaultInputBuilder inputBuilder = new(input);

        DataListInputBuilder sut = new(inputBuilder, dataList);

        //  Act
        sut.Attributes.Add(key, value);

        //  Assert
        using (new AssertionScope())
        {
            input.Attributes.Should().ContainKey(key);
            input.Attributes[key].Should().Be(value);

            dataList.Attributes.Should().BeEmpty();
        }
    }
}
