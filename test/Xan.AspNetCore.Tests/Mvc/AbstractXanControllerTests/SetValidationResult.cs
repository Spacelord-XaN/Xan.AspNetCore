using FluentValidation.Results;

namespace Xan.AspNetCore.Tests.Mvc.AbstractXanControllerTests;

public class SetValidationResult
{
    [Theory]
    [AutoData]
    public void EmptyResult_ShouldClearModelState(string key, string error)
    {
        //  Arrange
        TestController sut = new();
        sut.ModelState.AddModelError(key, error);

        //  Act
        sut.SetValidationResult(new ValidationResult());

        //  Assert
        using (new AssertionScope())
        {
            sut.ModelState.Should().BeEmpty();
            sut.ModelState.IsValid.Should().BeTrue();
        }
    }

    [Theory]
    [AutoData]
    public void NotEmptyResult_ShouldClearModelStateAndAddNew(string existingKey, string existingError, string newKey, string newError)
    {
        //  Arrange
        TestController sut = new();
        sut.ModelState.AddModelError(existingKey, existingError);
        ValidationResult validationResult = new();
        validationResult.Errors.Add(new ValidationFailure(newKey, newError));

        //  Act
        sut.SetValidationResult(validationResult);

        //  Assert
        using (new AssertionScope())
        {
            sut.ModelState.Should().ContainKey(newKey);
            sut.ModelState[newKey]!.Errors.Should().ContainSingle().Which.ErrorMessage.Should().Be(newError);
            sut.ModelState.IsValid.Should().BeFalse();
        }
    }
}
