using Xan.AspNetCore.EntityFrameworkCore;

namespace Xan.AspNetCore.Tests.EntityFrameworkCore.IEntityExtensionsTests;

public class OrderById
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task NotThereShouldReturnNull(TestEntity[] entities)
    {
        //  Arrange
        Db.TestEntities.AddRange(entities);
        await Db.SaveChangesAsync();

        //  Act
        IQueryable<TestEntity> result = Db.TestEntities.OrderById();

        //  Assert
        result.Should().BeInAscendingOrder(x => x.Id);
    }
}
