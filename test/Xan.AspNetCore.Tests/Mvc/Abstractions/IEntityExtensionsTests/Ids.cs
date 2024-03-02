using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.Tests.Mvc.Abstractions.IEntityExtensionsTests;

public class Ids
{
    private class MyEntity
        : IEntity
    {
        public int Id { get; set; }
    }

    [Fact]
    public void Empty_ShouldReturnEmpty()
    {
        // Arrange
        IEnumerable<IEntity> entities = [];

        // Act
        IEnumerable<int> ids = entities.Ids();

        // Assert
        ids.Should().BeEmpty();
    }

    [Theory]
    [AutoData]
    public void ShouldReturnAllIds(int[] ids)
    {
        //  Arrange
        IEnumerable<IEntity> entities = ids.Select(id => new MyEntity { Id = id });

        //  Act
        IEnumerable<int> result = entities.Ids();

        //  Assert
        result.Should().BeEquivalentTo(ids);
    }
}
