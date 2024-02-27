using Xan.AspNetCore.EntityFrameworkCore;

namespace Xan.AspNetCore.Tests.EntityFrameworkCore.IEntityExtensionsTests;

public class FirstOrDefaultByIdAsync
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task NotThereShouldReturnNull(TestEntity[] other, TestEntity toFind)
    {
        //  Arrange
        Db.TestEntities.AddRange(other);
        await Db.SaveChangesAsync();

        //  Act
        TestEntity? found = await Db.TestEntities.FirstOrDefaultByIdAsync(toFind.Id);

        //  Assert
        found.Should().BeNull();
    }

    [Theory]
    [AutoData]
    public async Task ShouldReturn_EntityWithId(TestEntity[] other, TestEntity toFind)
    {
        //  Arrange
        Db.TestEntities.AddRange(other);
        Db.TestEntities.Add(toFind);
        await Db.SaveChangesAsync();

        //  Act
        TestEntity? found = await Db.TestEntities.FirstOrDefaultByIdAsync(toFind.Id);

        //  Assert
        found.Should().Be(toFind);
    }
}
