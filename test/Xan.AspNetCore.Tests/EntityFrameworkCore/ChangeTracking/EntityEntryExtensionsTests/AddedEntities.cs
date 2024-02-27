using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xan.AspNetCore.EntityFrameworkCore.ChangeTracking;

namespace Xan.AspNetCore.Tests.EntityFrameworkCore.ChangeTracking.EntityEntryExtensionsTests;

public class AddedEntities
    : TestBase
{
    [Fact]
    public void Empty_ShouldReturnEmpty()
    {
        //  Arrange
        IEnumerable<EntityEntry> entries = [];

        //  Act
        IEnumerable<TestEntity> result = entries.AddedEntities<TestEntity>();

        //  Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [AutoData]
    public async Task Added_ShouldReturnEmpty(TestEntity[] entities, TestEntity[] added)
    {
        //  Arrange
        Db.TestEntities.AddRange(entities);
        await Db.SaveChangesAsync();
        Db.TestEntities.AddRange(added);

        //  Act
        IEnumerable<TestEntity> result = Db.ChangeTracker.Entries().AddedEntities<TestEntity>();

        //  Assert
        result.Should().BeEquivalentTo(added);
    }

    [Theory]
    [AutoData]
    public async Task NoAdded_ShouldReturnEmpty(TestEntity[] entities)
    {
        //  Arrange
        Db.TestEntities.AddRange(entities);
        await Db.SaveChangesAsync();

        //  Act
        IEnumerable<TestEntity> result = Db.ChangeTracker.Entries().AddedEntities<TestEntity>();

        //  Assert
        result.Should().BeEmpty();
    }
}
