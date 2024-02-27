using Xan.AspNetCore.EntityFrameworkCore;
using Xan.AspNetCore.Models;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public class DisableAsync
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldDisableAndRemoveFromChangeTracker(TestEntity entity)
    {
        // Arrange
        entity.State = ObjectState.Enabled;
        Db.Tests.Add(entity);
        await Db.SaveChangesAsync();

        //  Act
        await Sut.DisableAsync(entity.Id);

        //  Assert
        using (new AssertionScope())
        {
            Db.Tests.Local.Should().NotContain(entity);
            TestEntity fromDb = await Db.Tests.FirstByIdAsync(entity.Id);
            fromDb.State.Should().Be(ObjectState.Disabled);
        }
    }
}
