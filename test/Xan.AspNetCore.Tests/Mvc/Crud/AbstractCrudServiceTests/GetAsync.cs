namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public class GetAsync
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldGetUntracked(TestEntity entity)
    {
        // Arrange
        Db.Tests.Add(entity);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();

        //  Act
        TestEntity fromDb = await Sut.GetAsync(entity.Id);

        //  Assert
        using (new AssertionScope())
        {
            fromDb.Should().BeEquivalentTo(entity);
            Db.Tests.Local.Should().NotContain(fromDb);
        }
    }
}
