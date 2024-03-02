using Microsoft.AspNetCore.Mvc;
using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudControllerTests;

public class Create
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ReturnsNewObject(TestEntity entity)
    {
        //  Arrange
        ICrudModel model = A.Fake<ICrudModel>();

        A.CallTo(() => Service.CreateNewAsync())
            .Returns(entity);
        A.CallTo(() => ModelFactory.CreateModelAsync(entity))
            .Returns(model);
            
        //  Act
        IActionResult result = await Sut.Create();

        //  Assert
        using (new AssertionScope())
        {
            ViewResult view = result.Should().BeOfType<ViewResult>().Subject;
            view.ViewName.Should().Be("CrudCreate");
            view.ViewData.ModelState.IsValid.Should().BeTrue();
            view.Model.Should().Be(model);
        }

        A.CallTo(() => Service.CreateNewAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => ModelFactory.CreateModelAsync(entity)).MustHaveHappenedOnceExactly();
    }
}
