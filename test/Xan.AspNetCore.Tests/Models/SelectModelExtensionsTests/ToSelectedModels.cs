using Xan.AspNetCore.Models;

namespace Xan.AspNetCore.Tests.Models.SelectModelExtensionsTests;

public class ToSelectedModels
{
    [Fact]
    public void Empty_ShouldReturnEmpty()
    {
        //  Arrange
        IEnumerable<int> items = [];

        //  Act
        IEnumerable<SelectModel<int>> result = items.ToSelectedModels();

        //  Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [AutoData]
    public void NotEmpty_ShouldReturnNewModelsForEachElement(int[] items)
    {
        //  Arrange

        //  Act
        IEnumerable<SelectModel<int>> result = items.ToSelectedModels();

        //  Assert
        using (new AssertionScope())
        {
            result.Should().HaveCount(items.Length);
            result.Select(sm => sm.Value).Should().BeEquivalentTo(items);
        }
    }
}
