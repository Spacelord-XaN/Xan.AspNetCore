using Xan.AspNetCore.EntityFrameworkCore;
using Xan.AspNetCore.Models;
using Xan.AspNetCore.Mvc.Abstractions;

namespace Xan.AspNetCore.Tests.EntityFrameworkCore.ICrudEntityExtensionsTests;

public class WhereDisabled
{
    private class TestEntity
        : ICrudEntity
    {
        public ObjectState State { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    private readonly Fixture _fixture = new();

    [Fact]
    public void Empty_ShouldReturnEmpty()
    {
        //  Arrange
        IQueryable<TestEntity> entities = Enumerable.Empty<TestEntity>().AsQueryable();

        //  Act
        IEnumerable<TestEntity> result = entities.WhereDisabled();

        //  Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void NoDisabled_ShouldReturnEmpty()
    {
        //  Arrange
        IQueryable<TestEntity> entities = _fixture.Build<TestEntity>()
            .With(entity => entity.State, ObjectState.Enabled)
            .CreateMany()
            .AsQueryable();

        //  Act
        IEnumerable<TestEntity> result = entities.WhereDisabled();

        //  Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void SomeDisabled_ShouldReturnThem()
    {
        //  Arrange
        IEnumerable<TestEntity> enabled = _fixture.Build<TestEntity>()
            .With(entity => entity.State, ObjectState.Enabled)
            .CreateMany().ToArray();
        IEnumerable<TestEntity> disabled = _fixture.Build<TestEntity>()
            .With(entity => entity.State, ObjectState.Disabled)
            .CreateMany().ToArray();
        List<TestEntity> entities = [.. enabled, .. disabled];
        IQueryable<TestEntity> entitiesIq = entities.AsQueryable();

        //  Act
        IEnumerable<TestEntity> result = entitiesIq.WhereDisabled();

        //  Assert
        result.Should().BeEquivalentTo(disabled);
    }
}
