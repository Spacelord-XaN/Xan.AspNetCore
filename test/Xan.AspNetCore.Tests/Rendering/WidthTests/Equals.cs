using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.WidthTests;

public class Equals
{
    public static TheoryData<Width, Width> IsEqualData { get; } = new()
    {
        { Width.Auto, Width.Auto },
        { Width.Zero, Width.Zero },
        { Width.Percent(50), Width.Percent(50) }
    };

    [Theory]
    [MemberData(nameof(IsEqualData))]
    public void IsEqual(Width a, Width b)
    {
        //  Arrange

        //  Act
        bool result = a.Equals(b);

        //  Assert
        result.Should().BeTrue();
    }

    public static TheoryData<Width, Width> IsNotEqualData { get; } = new()
    {
        { Width.Auto, Width.Zero },
        { Width.Zero, Width.Percent(50) },
        { Width.Percent(50), Width.Auto }
    };

    [Theory]
    [MemberData(nameof(IsNotEqualData))]
    public void IsNotEqual(Width a, Width b)
    {
        //  Arrange

        //  Act
        bool result = a.Equals(b);

        //  Assert
        result.Should().BeFalse();
    }
}
