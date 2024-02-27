using Microsoft.EntityFrameworkCore;
using Xan.AspNetCore.EntityFrameworkCore;
using Xan.Extensions;

namespace Xan.AspNetCore.Tests.EntityFrameworkCore.DbContextExtensionsTests;

public class AddTimestampHandler
    : TestBase
{
    private readonly IClock _clock;

    public AddTimestampHandler()
    {
        _clock = X.StrictFake<IClock>();
    }

    [Theory]
    [AutoData]
    public async void Added_ShouldSetCreatedAndUpdatedAt(TestEntity[] entities, DateTime timestamp)
    {
        //  Arrange
        A.CallTo(() => _clock.GetCurrentDateTime()).Returns(timestamp);
        Db.AddTimestampHandler(_clock);
        Db.TestEntities.AddRange(entities);

        //  Act
        await Db.SaveChangesAsync();

        //  Assert
        Db.TestEntities.Should().AllSatisfy(entity =>
        {
            entity.CreatedAt.Should().Be(timestamp);
            entity.UpdatedAt.Should().Be(timestamp);
        });
    }

    [Theory]
    [AutoData]
    public async void Modified_ShouldSetOnlyUpdatedAt(TestEntity[] entities, DateTime createdAt, DateTime updatedAt)
    {
        //  Arrange
        A.CallTo(() => _clock.GetCurrentDateTime()).Returns(createdAt);
        Db.AddTimestampHandler(_clock);
        Db.TestEntities.AddRange(entities);
        await Db.SaveChangesAsync();
        A.CallTo(() => _clock.GetCurrentDateTime()).Returns(updatedAt);
        foreach (TestEntity entity in Db.TestEntities)
        {
            Db.Entry(entity).State = EntityState.Modified;
        }

        //  Act
        await Db.SaveChangesAsync();

        //  Assert
        Db.TestEntities.Should().AllSatisfy(entity =>
        {
            entity.CreatedAt.Should().Be(createdAt);
            entity.UpdatedAt.Should().Be(updatedAt);
        });
    }
}
