using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Tests.Parameter.ListParameterTests;

public class ToPageSize
{
    [Theory]
    [AutoData]
    public void ShouldReturnClone_WithPageSizeSet(ListParameter other, int pageSize)
    {
        //  Arrange

        //  Act
        ListParameter result = other.ToPageSize(pageSize);

        //  Assert
        using (new AssertionScope())
        {
            result.SearchString.Should().Be(other.SearchString);
            result.PageIndex.Should().Be(other.PageIndex);
            result.PageSize.Should().Be(pageSize);
            result.State.Should().Be(other.State);
        }
    }
}
