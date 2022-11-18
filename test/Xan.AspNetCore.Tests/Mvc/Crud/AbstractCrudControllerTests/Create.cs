using Microsoft.AspNetCore.Mvc;
using Moq;
using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudControllerTests;

public class Create
    : AbstractCrudControllerTest
{
    [Fact]
    public async Task ReturnsNewObject()
    {
        TestEntity entity = Fixture.Create<TestEntity>();
        ICrudModel crudModel = Fixture.Create<ICrudModel>();

        MockModelFactory.Setup(f => f.CreateModelAsync(entity))
            .ReturnsAsync(crudModel);
        MockService.Setup(s => s.CreateNewAsync())
            .ReturnsAsync(entity);

        IActionResult result = await Sut.Create();

        ViewResult viewResult = result.Should()
            .BeOfType<ViewResult>().Subject;
        viewResult.ViewName.Should()
            .Be("CrudCreate");
        viewResult.Model.Should()
            .BeAssignableTo<ICrudModel>();

        MockService.Verify(s => s.CreateNewAsync(), Times.Once());
        MockModelFactory.Verify(f => f.CreateModelAsync(It.IsAny<TestEntity>()), Times.Once());
    }
}
