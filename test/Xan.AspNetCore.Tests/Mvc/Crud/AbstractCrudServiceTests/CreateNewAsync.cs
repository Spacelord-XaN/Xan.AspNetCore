namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public class CreateNewAsync
    : TestBase
{
    [Fact]
    public async Task ReturnsDefaultObject()
    {
        // Arrange

        //  Act
        TestEntity entity = await Sut.CreateNewAsync();

        //  Assert
        entity.Should().BeEquivalentTo(new TestEntity());
    }
}
