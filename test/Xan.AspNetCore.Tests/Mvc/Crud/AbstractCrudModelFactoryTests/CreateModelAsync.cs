using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudModelFactoryTests;

public class CreateModelAsync
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldReturnNewModel(TestEntity entity)
    {
        //  Arrange

        //  Act
        ICrudModel result = await Sut.CreateModelAsync(entity);

        //  Assert
        using (new AssertionScope())
        {
            CrudModel<TestEntity> crudModel = result.Should().BeOfType<CrudModel<TestEntity>>().Subject;

            crudModel.Entity.Should().Be(entity);
            crudModel.Title.Should().Be(TestCrudModelFactory.CreateTitleString);
        }
    }
}
