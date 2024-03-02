using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Tests.Parameter.ListParameterTests;

public class ToPage
{
    [Theory]
    [AutoData]
    public void ShouldReturnClone_WithPageIndexSet(ListParameter other, int page)
    {
        //  Arrange

        //  Act
        ListParameter result = other.ToPage(page);

        //  Assert
        using (new AssertionScope())
        {
            result.SearchString.Should().Be(other.SearchString);
            result.PageIndex.Should().Be(page);
            result.PageSize.Should().Be(other.PageSize);
            result.State.Should().Be(other.State);
        }
    }
}
