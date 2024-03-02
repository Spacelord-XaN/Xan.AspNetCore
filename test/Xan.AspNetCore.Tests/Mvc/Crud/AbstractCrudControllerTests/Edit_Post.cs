using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudControllerTests;

public class Edit_Post
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task NotValid_ShouldReturnView(TestEntity entity, string origin)
    {
        //  Arrange
        ICrudModel model = A.Fake<ICrudModel>();

        A.CallTo(() => Validator.ValidateAsync(entity, default)).Returns(new ValidationResult { Errors = { new ValidationFailure("Property", "Error") } });
        A.CallTo(() => ModelFactory.EditModelAsync(entity)).Returns(model);
            
        //  Act
        IActionResult result = await Sut.Edit(entity, origin);

        //  Assert
        using (new AssertionScope())
        {
            ViewResult view = result.Should().BeOfType<ViewResult>().Subject;
            view.ViewName.Should().Be("CrudEdit");
            view.ViewData.ModelState.IsValid.Should().BeFalse();
            view.Model.Should().Be(model);
        }

        A.CallTo(() => Validator.ValidateAsync(entity, default)).MustHaveHappenedOnceExactly();
        A.CallTo(() => ModelFactory.EditModelAsync(entity)).MustHaveHappenedOnceExactly();
    }

    [Theory]
    [AutoData]
    public async Task Valid_ShouldReturnRedirectToList(TestEntity entity, string origin, string listUrl)
    {
        //  Arrange
        A.CallTo(() => Validator.ValidateAsync(entity, default)).Returns(new ValidationResult());
        A.CallTo(() => Service.UpdateAsync(entity)).DoesNothing();
        A.CallTo(() => Router.ToList()).Returns(listUrl);

        //  Act
        IActionResult result = await Sut.Edit(entity, origin);

        //  Assert
        using (new AssertionScope())
        {
            RedirectResult redirect = result.Should().BeOfType<RedirectResult>().Subject;
            redirect.Url.Should().Be(listUrl);
        }

        A.CallTo(() => Validator.ValidateAsync(entity, default)).MustHaveHappenedOnceExactly();
        A.CallTo(() => Service.UpdateAsync(entity)).MustHaveHappenedOnceExactly();
        A.CallTo(() => Router.ToList()).MustHaveHappenedOnceExactly();
    }
}
