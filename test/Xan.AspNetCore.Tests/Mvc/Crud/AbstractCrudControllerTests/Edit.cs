using Microsoft.AspNetCore.Mvc;
using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudControllerTests;

public class Edit
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldReturnView(TestEntity entity)
    {
        //  Arrange
        ICrudModel model = A.Fake<ICrudModel>();

        A.CallTo(() => Service.GetAsync(entity.Id))
            .Returns(entity);
        A.CallTo(() => ModelFactory.EditModelAsync(entity))
            .Returns(model);
            
        //  Act
        IActionResult result = await Sut.Edit(entity.Id);

        //  Assert
        using (new AssertionScope())
        {
            ViewResult view = result.Should().BeOfType<ViewResult>().Subject;
            view.ViewName.Should().Be("CrudEdit");
            view.ViewData.ModelState.IsValid.Should().BeTrue();
            view.Model.Should().Be(model);
        }

        A.CallTo(() => Service.GetAsync(entity.Id)).MustHaveHappenedOnceExactly();
        A.CallTo(() => ModelFactory.EditModelAsync(entity)).MustHaveHappenedOnceExactly();
    }
}
