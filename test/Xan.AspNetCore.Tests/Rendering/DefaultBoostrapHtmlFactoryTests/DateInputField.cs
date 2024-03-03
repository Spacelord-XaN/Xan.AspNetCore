using Microsoft.AspNetCore.Html;

namespace Xan.AspNetCore.Tests.Rendering.DefaultBoostrapHtmlFactoryTests;

public class DateInputField
    : TestBase
{
    [Theory]
    [AutoData]
    public void DateOnly_InvalidState_AutoFocus(string name, string title, string error1, string error2)
    {
        // Arrange
        ViewContext.ModelState.AddModelError(name, error1);
        ViewContext.ModelState.AddModelError(name, error2);

        // Act
        IHtmlContent result = Sut.DateInputField(name, new DateOnly(2063, 04, 05), title, true);

        //  Assert
        result.Should().BeHtml($"""<div class="mb-3"><label class="form-label">{title}</label><input autofocus="" class="form-control is-invalid" id="id_{name}" name="{name}" type="date" value="2063-04-05"></input><div class="invalid-feedback">{error1}"""
            .Line($"""{error2}</div></div>"""));
    }

    [Theory]
    [AutoData]
    public void DateOnly_InvalidState_NoAutoFocus(string name, string title, string error1, string error2)
    {
        // Arrange
        ViewContext.ModelState.AddModelError(name, error1);
        ViewContext.ModelState.AddModelError(name, error2);

        // Act
        IHtmlContent result = Sut.DateInputField(name, new DateOnly(2063, 04, 05), title, false);

        //  Assert
        result.Should().BeHtml($"""<div class="mb-3"><label class="form-label">{title}</label><input class="form-control is-invalid" id="id_{name}" name="{name}" type="date" value="2063-04-05"></input><div class="invalid-feedback">{error1}"""
            .Line($"""{error2}</div></div>"""));
    }
    
    [Theory]
    [AutoData]
    public void DateOnly_ValidState_AutoFocus(string name, string title)
    {
        // Arrange

        // Act
        IHtmlContent result = Sut.DateInputField(name, new DateOnly(2063, 04, 05), title, true);

        //  Assert
        result.Should().BeHtml($"""<div class="mb-3"><label class="form-label">{title}</label><input autofocus="" class="form-control" id="id_{name}" name="{name}" type="date" value="2063-04-05"></input></div>""");
    }

    [Theory]
    [AutoData]
    public void DateOnly_ValidState_NoAutoFocus(string name, string title)
    {
        // Arrange

        // Act
        IHtmlContent result = Sut.DateInputField(name, new DateOnly(2063, 04, 05), title, false);

        //  Assert
        result.Should().BeHtml($"""<div class="mb-3"><label class="form-label">{title}</label><input class="form-control" id="id_{name}" name="{name}" type="date" value="2063-04-05"></input></div>""");
    }

    [Theory]
    [AutoData]
    public void DateTime_InvalidState_AutoFocus(string name, string title, string error1, string error2)
    {
        // Arrange
        ViewContext.ModelState.AddModelError(name, error1);
        ViewContext.ModelState.AddModelError(name, error2);

        // Act
        IHtmlContent result = Sut.DateInputField(name, new DateTime(2063, 04, 05, 11, 22, 33, 444), title, true);

        //  Assert
        result.Should().BeHtml($"""<div class="mb-3"><label class="form-label">{title}</label><input autofocus="" class="form-control is-invalid" id="id_{name}" name="{name}" type="date" value="2063-04-05"></input><div class="invalid-feedback">{error1}"""
            .Line($"""{error2}</div></div>"""));
    }

    [Theory]
    [AutoData]
    public void DateTime_InvalidState_NoAutoFocus(string name, string title, string error1, string error2)
    {
        // Arrange
        ViewContext.ModelState.AddModelError(name, error1);
        ViewContext.ModelState.AddModelError(name, error2);

        // Act
        IHtmlContent result = Sut.DateInputField(name, new DateTime(2063, 04, 05, 11, 22, 33, 444), title, false);

        //  Assert
        result.Should().BeHtml($"""<div class="mb-3"><label class="form-label">{title}</label><input class="form-control is-invalid" id="id_{name}" name="{name}" type="date" value="2063-04-05"></input><div class="invalid-feedback">{error1}"""
            .Line($"""{error2}</div></div>"""));
    }

    [Theory]
    [AutoData]
    public void DateTime_ValidState_AutoFocus(string name, string title)
    {
        // Arrange

        // Act
        IHtmlContent result = Sut.DateInputField(name, new DateTime(2063, 04, 05, 11, 22, 33, 444), title, true);

        //  Assert
        result.Should().BeHtml($"""<div class="mb-3"><label class="form-label">{title}</label><input autofocus="" class="form-control" id="id_{name}" name="{name}" type="date" value="2063-04-05"></input></div>""");
    }

    [Theory]
    [AutoData]
    public void DateTime_ValidState_NoAutoFocus(string name, string title)
    {
        // Arrange

        // Act
        IHtmlContent result = Sut.DateInputField(name, new DateTime(2063, 04, 05, 11, 22, 33, 444), title, false);

        //  Assert
        result.Should().BeHtml($"""<div class="mb-3"><label class="form-label">{title}</label><input class="form-control" id="id_{name}" name="{name}" type="date" value="2063-04-05"></input></div>""");
    }
}
