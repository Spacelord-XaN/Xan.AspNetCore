namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudServiceTests;

public class CreateNewAsync
    : AbstractCrudServiceTest
{
    [Fact]
    public async Task ReturnsDefaultObject()
    {
        TestCrudService sut = new(Context);

        TestEntity entity = await sut.CreateNewAsync();

        entity.Should().BeEquivalentTo(new TestEntity());
    }
}
