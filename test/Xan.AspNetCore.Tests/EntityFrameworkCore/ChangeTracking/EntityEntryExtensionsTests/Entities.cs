using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xan.AspNetCore.EntityFrameworkCore.ChangeTracking;

namespace Xan.AspNetCore.Tests.EntityFrameworkCore.ChangeTracking.EntityEntryExtensionsTests;

public class Entities
    : TestBase
{
    [Theory]
    [AutoData]
    public void Empty_ShouldReturnEmpty(EntityState state)
    {
        //  Arrange
        IEnumerable<EntityEntry> entries = [];

        //  Act
        IEnumerable<TestEntity> result = entries.Entities<TestEntity>(state);

        //  Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [AutoData]
    public async Task WithState_ShouldReturnEmpty(TestEntity[] entities, TestEntity[] withState)
    {
        //  Arrange
        Db.TestEntities.AddRange(entities);
        await Db.SaveChangesAsync();
        Db.TestEntities.AddRange(withState);

        //  Act
        IEnumerable<TestEntity> result = Db.ChangeTracker.Entries().Entities<TestEntity>(EntityState.Added);

        //  Assert
        result.Should().BeEquivalentTo(withState);
    }

    [Theory]
    [AutoData]
    public async Task NoWithStateModifed_ShouldReturnEmpty(TestEntity[] entities, EntityState state)
    {
        //  Arrange
        Db.TestEntities.AddRange(entities);
        await Db.SaveChangesAsync();

        //  Act
        IEnumerable<TestEntity> result = Db.ChangeTracker.Entries().Entities<TestEntity>(state);

        //  Assert
        result.Should().BeEmpty();
    }
}
