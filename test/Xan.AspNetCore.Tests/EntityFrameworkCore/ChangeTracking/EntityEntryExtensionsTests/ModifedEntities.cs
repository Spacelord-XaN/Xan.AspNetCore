using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xan.AspNetCore.EntityFrameworkCore.ChangeTracking;

namespace Xan.AspNetCore.Tests.EntityFrameworkCore.ChangeTracking.EntityEntryExtensionsTests;

public class ModifedEntities
    : TestBase
{
    [Fact]
    public void Empty_ShouldReturnEmpty()
    {
        //  Arrange
        IEnumerable<EntityEntry> entries = [];

        //  Act
        IEnumerable<TestEntity> result = entries.ModifedEntities<TestEntity>();

        //  Assert
        result.Should().BeEmpty();
    }

    [Theory]
    [AutoData]
    public async Task Modified_ShouldReturnEmpty(TestEntity[] entities, TestEntity[] modified)
    {
        //  Arrange
        Db.TestEntities.AddRange(entities);
        await Db.SaveChangesAsync();
        Db.TestEntities.AddRange(modified);
        foreach (TestEntity entity in modified)
        {
            Db.Entry(entity).State = EntityState.Modified;
        }

        //  Act
        IEnumerable<TestEntity> result = Db.ChangeTracker.Entries().ModifedEntities<TestEntity>();

        //  Assert
        result.Should().BeEquivalentTo(modified);
    }

    [Theory]
    [AutoData]
    public async Task NoModifed_ShouldReturnEmpty(TestEntity[] entities)
    {
        //  Arrange
        Db.TestEntities.AddRange(entities);
        await Db.SaveChangesAsync();

        //  Act
        IEnumerable<TestEntity> result = Db.ChangeTracker.Entries().ModifedEntities<TestEntity>();

        //  Assert
        result.Should().BeEmpty();
    }
}
