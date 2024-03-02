using Xan.AspNetCore.Models;

namespace Xan.AspNetCore.Tests.Models.SelectModelTests;

public class Ctor
{
    [Fact]
    public void NoArgs_ShouldInitWithDefaults()
    {
        //  Arrange

        //  Act
        SelectModel<int> sut = new();

        //  Assert
        using (new AssertionScope())
        {
            sut.IsSelected.Should().BeFalse();
            sut.Value.Should().Be(default);
        }
    }

    [Theory]
    [AutoData]
    public void WithArg_ShouldInitWithArg(int value)
    {
        //  Arrange

        //  Act
        SelectModel<int> sut = new(value);

        //  Assert
        using (new AssertionScope())
        {
            sut.IsSelected.Should().BeFalse();
            sut.Value.Should().Be(value);
        }
    }

    [Fact]
    public void WithNull_ShouldThrowException()
    {
        //  Arrange

        //  Act
        Action act = () => new SelectModel<int?>(null);

        //  Assert
        act.Should().Throw<ArgumentNullException>();
    }
}
