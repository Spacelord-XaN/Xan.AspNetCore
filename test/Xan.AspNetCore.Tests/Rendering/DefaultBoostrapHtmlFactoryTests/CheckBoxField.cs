using Microsoft.AspNetCore.Html;

namespace Xan.AspNetCore.Tests.Rendering.DefaultBoostrapHtmlFactoryTests;

public class CheckBoxField
    : TestBase
{
    [Theory]
    [AutoData]
    public void InvalidState(string name, string title, string error1, string error2)
    {
        // Arrange
        ViewContext.ModelState.AddModelError(name, error1);
        ViewContext.ModelState.AddModelError(name, error2);

        // Act
        IHtmlContent result = Sut.CheckBoxField(name, true, title);

        //  Assert
        result.Should().BeHtml($"""<div class="form-check mb-3"><input checked="" class="form-check-input is-invalid" id="id_{name}" name="{name}" type="checkbox" value="true"></input><label class="form-check-label">{title}</label><div class="invalid-feedback">{error1}"""
            .Line($"""{error2}</div></div>"""));
    }
    
    [Theory]
    [AutoData]
    public void ValidState(string name, string title)
    {
        // Arrange

        // Act
        IHtmlContent result = Sut.CheckBoxField(name, true, title);

        //  Assert
        result.Should().BeHtml($"""<div class="form-check mb-3"><input checked="" class="form-check-input" id="id_{name}" name="{name}" type="checkbox" value="true"></input><label class="form-check-label">{title}</label></div>""");
    }
}
