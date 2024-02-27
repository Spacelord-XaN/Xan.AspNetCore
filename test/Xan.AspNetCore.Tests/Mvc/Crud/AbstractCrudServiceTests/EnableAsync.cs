using Xan.AspNetCore.EntityFrameworkCore;
using Xan.AspNetCore.Models;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public class EnableAsync
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldEnableAndRemoveFromChangeTracker(TestEntity entity)
    {
        // Arrange
        entity.State = ObjectState.Disabled;
        Db.Tests.Add(entity);
        await Db.SaveChangesAsync();

        //  Act
        await Sut.EnableAsync(entity.Id);

        //  Assert
        using (new AssertionScope())
        {
            Db.Tests.Local.Should().NotContain(entity);
            TestEntity fromDb = await Db.Tests.FirstByIdAsync(entity.Id);
            fromDb.State.Should().Be(ObjectState.Enabled);
        }
    }
}
