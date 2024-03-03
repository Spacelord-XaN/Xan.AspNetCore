using Microsoft.AspNetCore.Html;

namespace Xan.AspNetCore.Tests.Rendering.DefaultBoostrapHtmlFactoryTests;

public class NonFieldValidationResults
    : TestBase
{
    [Fact]
    public void IsValid_ShouldReturnEmpty()
    {
        //  Arrange

        //  Act
        IHtmlContent result = Sut.NonFieldValidationResults();

        //  Assert
        result.Should().BeHtml("");
    }

    [Theory]
    [AutoData]
    public void NotValid_ShouldReturnAllErrors(string key1, string key1Error1, string key1Error2, string key2, string key2Error1, string key2Error2)
    {
        //  Arrange
        ViewContext.ModelState.AddModelError(key2, key2Error1);
        ViewContext.ModelState.AddModelError(key1, key1Error2);
        ViewContext.ModelState.AddModelError(key1, key1Error1);
        ViewContext.ModelState.AddModelError(key2, key2Error2);

        //  Act
        IHtmlContent result = Sut.NonFieldValidationResults();

        //  Assert
        result.Should().BeHtml(key1Error2
            .Line(key1Error1)
            .Line(key2Error1)
            .Line(key2Error2)
            .Line());
    }
}
