using Xan.AspNetCore.EntityFrameworkCore;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public class UpdateAsync
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldUpdate(TestEntity testEntity, string newName)
    {
        //  Arrange
        Db.Tests.Add(testEntity);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();
        testEntity.Name = newName;

        //  Act
        await Sut.UpdateAsync(testEntity);

        //  Assert
        using (new AssertionScope())
        {
            Db.ChangeTracker.Clear();
            TestEntity fromDb = await Db.Tests.FirstByIdAsync(testEntity.Id);
            fromDb.Should().BeEquivalentTo(testEntity);
        }   
    }
}
