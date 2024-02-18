using Xan.AspNetCore.EntityFrameworkCore;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public class DisableAsync
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldDisableAndRemoveFromChangeTracker(TestEntity entity)
    {
        // Arrange
        entity.State = Models.ObjectState.Enabled;
        Db.Tests.Add(entity);
        await Db.SaveChangesAsync();

        //  Act
        await Sut.DisableAsync(entity.Id);

        //  Assert
        using (new AssertionScope())
        {
            Db.Tests.Local.Should().NotContain(entity);
            TestEntity fromDb = await Db.Tests.FirstByIdAsync(entity.Id);
            fromDb.State.Should().Be(Models.ObjectState.Disabled);
        }
    }
}
