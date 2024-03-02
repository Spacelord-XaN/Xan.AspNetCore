using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudControllerTests;

public class Create_Post
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task NotValid_ShouldReturnView(TestEntity entity)
    {
        //  Arrange
        ICrudModel model = A.Fake<ICrudModel>();

        A.CallTo(() => Validator.ValidateAsync(entity, default)).Returns(new ValidationResult { Errors = { new ValidationFailure("Property", "Error") } });
        A.CallTo(() => ModelFactory.CreateModelAsync(entity)).Returns(model);
            
        //  Act
        IActionResult result = await Sut.Create(entity);

        //  Assert
        using (new AssertionScope())
        {
            ViewResult view = result.Should().BeOfType<ViewResult>().Subject;
            view.ViewName.Should().Be("CrudCreate");
            view.ViewData.ModelState.IsValid.Should().BeFalse();
            view.Model.Should().Be(model);
        }

        A.CallTo(() => Validator.ValidateAsync(entity, default)).MustHaveHappenedOnceExactly();
        A.CallTo(() => ModelFactory.CreateModelAsync(entity)).MustHaveHappenedOnceExactly();
    }

    [Theory]
    [AutoData]
    public async Task Valid_ShouldReturnRedirectToList(TestEntity entity, int id, string url)
    {
        //  Arrange
        A.CallTo(() => Validator.ValidateAsync(entity, default)).Returns(new ValidationResult());
        A.CallTo(() => Service.CreateAsync(entity)).Returns(id);
        A.CallTo(() => Router.ToList()).Returns(url);

        //  Act
        IActionResult result = await Sut.Create(entity);

        //  Assert
        using (new AssertionScope())
        {
            RedirectResult redirect = result.Should().BeOfType<RedirectResult>().Subject;
            redirect.Url.Should().Be(url);
        }

        A.CallTo(() => Validator.ValidateAsync(entity, default)).MustHaveHappenedOnceExactly();
        A.CallTo(() => Service.CreateAsync(entity)).MustHaveHappenedOnceExactly();
        A.CallTo(() => Router.ToList()).MustHaveHappenedOnceExactly();
    }
}
