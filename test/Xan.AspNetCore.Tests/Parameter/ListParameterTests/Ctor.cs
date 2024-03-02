using Xan.AspNetCore.Models;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Tests.Parameter.ListParameterTests;

public class Ctor
{
    [Fact]
    public void ShouldInitDefaults()
    {
        // Arrange

        // Act
        ListParameter parameter = new ();

        // Assert
        using (new AssertionScope())
        {
            parameter.SearchString.Should().BeNull();
            parameter.PageIndex.Should().Be(1);
            parameter.PageSize.Should().BeNull();
            parameter.State.Should().Be(ObjectState.Enabled);
        }
    }

    [Theory]
    [AutoData]
    public void ShouldCopyValues(ListParameter other)
    {
        // Arrange

        // Act
        ListParameter parameter = new(other);

        // Assert
        using (new AssertionScope())
        {
            parameter.SearchString.Should().Be(other.SearchString);
            parameter.PageIndex.Should().Be(other.PageIndex);
            parameter.PageSize.Should().Be(other.PageSize);
            parameter.State.Should().Be(other.State);
        }
    }
}
