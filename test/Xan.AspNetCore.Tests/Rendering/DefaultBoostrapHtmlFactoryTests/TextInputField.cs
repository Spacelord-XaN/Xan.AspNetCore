﻿using Microsoft.AspNetCore.Html;

namespace Xan.AspNetCore.Tests.Rendering.DefaultBoostrapHtmlFactoryTests;

public class TextInputField
    : TestBase
{
    [Theory]
    [AutoData]
    public void InvalidState_AutoFocus(string name, string value, string title, string error1, string error2)
    {
        // Arrange
        ViewContext.ModelState.AddModelError(name, error1);
        ViewContext.ModelState.AddModelError(name, error2);

        // Act
        IHtmlContent result = Sut.TextInputField(name, value, title, true);

        //  Assert
        result.Should().BeHtml($"""<div class="mb-3"><label class="form-label">{title}</label><input autofocus="" class="form-control is-invalid" id="id_{name}" name="{name}" type="text" value="{value}"></input><div class="invalid-feedback">{error1}"""
            .Line($"""{error2}</div></div>"""));
    }

    [Theory]
    [AutoData]
    public void InvalidState_NoAutoFocus(string name, string value, string title, string error1, string error2)
    {
        // Arrange
        ViewContext.ModelState.AddModelError(name, error1);
        ViewContext.ModelState.AddModelError(name, error2);

        // Act
        IHtmlContent result = Sut.TextInputField(name, value, title, false);

        //  Assert
        result.Should().BeHtml($"""<div class="mb-3"><label class="form-label">{title}</label><input class="form-control is-invalid" id="id_{name}" name="{name}" type="text" value="{value}"></input><div class="invalid-feedback">{error1}"""
            .Line($"""{error2}</div></div>"""));
    }
    
    [Theory]
    [AutoData]
    public void ValidState_AutoFocus(string name, string value, string title)
    {
        // Arrange

        // Act
        IHtmlContent result = Sut.TextInputField(name, value, title, true);

        //  Assert
        result.Should().BeHtml($"""<div class="mb-3"><label class="form-label">{title}</label><input autofocus="" class="form-control" id="id_{name}" name="{name}" type="text" value="{value}"></input></div>""");
    }

    [Theory]
    [AutoData]
    public void ValidState_NoAutoFocus(string name, string value, string title)
    {
        // Arrange

        // Act
        IHtmlContent result = Sut.TextInputField(name, value, title, false);

        //  Assert
        result.Should().BeHtml($"""<div class="mb-3"><label class="form-label">{title}</label><input class="form-control" id="id_{name}" name="{name}" type="text" value="{value}"></input></div>""");
    }
}
