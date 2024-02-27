using Xan.AspNetCore.EntityFrameworkCore;

namespace Xan.AspNetCore.Tests.EntityFrameworkCore.IEntityExtensionsTests;

public class FirstByIdAsync
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldReturn_EntityWithId(TestEntity[] other, TestEntity toFind)
    {
        //  Arrange
        Db.TestEntities.AddRange(other);
        Db.TestEntities.Add(toFind);
        await Db.SaveChangesAsync();

        //  Act
        TestEntity found = await Db.TestEntities.FirstByIdAsync(toFind.Id);

        //  Assert
        found.Should().Be(toFind);
    }
}
