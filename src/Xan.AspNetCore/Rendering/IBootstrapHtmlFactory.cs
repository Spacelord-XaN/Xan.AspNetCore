using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Xan.AspNetCore.Rendering;

public interface IBootstrapHtmlFactory
    : IHtmlFactory
    , IViewContextAware
{
    TagBuilder CheckBoxField(string name, bool value, string title);

    TagBuilder DataListField(string name, string? value, ISet<string?> values, string title, bool autoFocus = false);

    TagBuilder DateInputField(string name, DateOnly value, string title, bool autoFocus = false);

    TagBuilder DateInputField(string name, DateTime value, string title, bool autoFocus = false);

    TagBuilder DateTimeInputField(string name, DateTime value, string title, bool autoFocus = false);

    string EnabledCss(bool isEnabled);

    TagBuilder EmailInputField(string name, string? value, string title, bool autoFocus = false);

    IHtmlContent NumberInputField(string name, int value, string title, bool autoFocus = false);

    IHtmlContent NumberInputField(string name, double value, string title, bool autoFocus = false);

    IHtmlContent NumberInputField(string name, decimal value, string title, bool autoFocus = false);

    TagBuilder PasswordInputField(string name, string? value, string title, bool autoFocus = false);

    TagBuilder PhoneNumberInputField(string name, string? value, string title, bool autoFocus = false);

    TagBuilder SelectField<TEnum>(string name, TEnum? value, SelectList items, string title, bool submitOnChange = false, bool autoFocus = false);

    TagBuilder TextAreaField(string name, string? value, string title, bool autoFocus = false);

    TagBuilder TextInputField(string name, string? value, string title, bool autoFocus = false);

    TagBuilder TimeInputField(string name, TimeOnly value, string title, bool autoFocus = false);

    IHtmlContent TitleCardHeader();

    IHtmlContent NonFieldValidationResults();
}
