using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.WidthTests;

public class ToString
{
    [Fact]
    public void Auto()
    {
        //  Arrange
        Width sut = Width.Auto;

        //  Act
        string result = sut.ToString();

        //  Assert
        result.Should().Be("Auto");
    }

    [Fact]
    public void Zero()
    {
        //  Arrange
        Width sut = Width.Zero;

        //  Act
        string result = sut.ToString();

        //  Assert
        result.Should().Be("0px");
    }

    [Theory]
    [AutoData]
    public void Percent(int percent)
    {
        //  Arrange
        Width sut = Width.Percent(percent);

        //  Act
        string result = sut.ToString();

        //  Assert
        result.Should().Be($"{percent}%");
    }
}
