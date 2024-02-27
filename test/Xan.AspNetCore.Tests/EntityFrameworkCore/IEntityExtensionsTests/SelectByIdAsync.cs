using Xan.AspNetCore.EntityFrameworkCore;

namespace Xan.AspNetCore.Tests.EntityFrameworkCore.IEntityExtensionsTests;

public class SelectByIdAsync
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
        string resutl = await Db.TestEntities.SelectByIdAsync(toFind.Id, entity => entity.Name);

        //  Assert
        resutl.Should().Be(toFind.Name);
    }
}
