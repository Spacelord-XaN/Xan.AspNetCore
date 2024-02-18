namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public class DeleteAsync
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldDeleteFromDb(TestEntity entity)
    {
        // Arrange
        Db.Tests.Add(entity);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();

        //  Act
        await Sut.DeleteAsync(entity.Id);

        //  Assert
        using (new AssertionScope())
        {
            Db.Tests.Should().NotContain(entity);
        }
    }
}
