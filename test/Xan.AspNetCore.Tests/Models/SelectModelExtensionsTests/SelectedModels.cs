using Xan.AspNetCore.Models;

namespace Xan.AspNetCore.Tests.Models.SelectModelExtensionsTests;

public class SelectedModels
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Empty_ShouldReturnEmpty()
    {
        //  Arrange
        IEnumerable<SelectModel<int>> selectModels = [];

        //  Act
        IEnumerable<int> result = selectModels.SelectedModels();

        //  Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [AutoData]
    public void NoSelected_ShouldReturnEmpty(int[] values)
    {
        //  Arrange
        IEnumerable<SelectModel<int>> models = values.Select(x => new SelectModel<int>(x));

        //  Act
        IEnumerable<int> result = models.SelectedModels();

        //  Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [AutoData]
    public void ShouldReturnSelected(int[] selected, int[] notSelected)
    {
        //  Arrange
        List<SelectModel<int>> models = selected.Select(x => new SelectModel<int>(x) {  IsSelected = true}).ToList();
        models.AddRange(notSelected.Select(x => new SelectModel<int>(x)));

        //  Act
        IEnumerable<int> result = models.SelectedModels();

        //  Assert
        result.Should().BeEquivalentTo(selected);
    }
}
