using Xan.AspNetCore.EntityFrameworkCore;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public class CreateAsync
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldInsertEntity_AndReturnId(TestEntity entity)
    {
        // Arrange

        //  Act
        int id = await Sut.CreateAsync(entity);

        //  Assert
        using (new AssertionScope())
        {
            id.Should().Be(entity.Id);
            Db.ChangeTracker.Clear();
            TestEntity fromDb = await Db.Tests.FirstByIdAsync(id);
            fromDb.Should().BeEquivalentTo(entity);
        }
    }
}
