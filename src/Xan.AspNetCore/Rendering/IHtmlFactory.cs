using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace Xan.AspNetCore.Rendering;

public interface IHtmlFactory
{
    IStringLocalizer Localizer { get; }

    TagBuilder CheckBox(string name, bool value);

    TagBuilder DateInput(string name, DateOnly value, bool autoFocus = false);
    
    TagBuilder DateInput(string name, DateTime value, bool autoFocus = false);

    TagBuilder Div();

    TagBuilder Heading(int level);

    TagBuilder HiddenInput(string name, int value);

    TagBuilder HiddenInput(string name, string value);

    string Id(string name);

    TagBuilder Input(string type, string name, string? value, bool autoFocus = false);

    TagBuilder Label();

    TagBuilder Link(string url);

    TagBuilder NumberInput(string name, int value, bool autoFocus = false);

    TagBuilder NumberInput(string name, double value, bool autoFocus = false);

    TagBuilder NumberInput(string name, decimal value, bool autoFocus = false);

    TagBuilder Option(string? value);

    TagBuilder Paragraph();

    TagBuilder PasswordInput(string name, string? value, bool autoFocus = false);

    TagBuilder Select(string name, string? value, SelectList items, bool submitOnChange = false, bool autoFocus = false);

    TagBuilder Select(string name, int? value, SelectList items, bool submitOnChange = false, bool autoFocus = false);

    TagBuilder Select<TEnum>(string name, TEnum? value, SelectList items, bool submitOnChange = false, bool autoFocus = false);

    TagBuilder SelectOption(string text, string? value, bool isSelected = false);

    TagBuilder Span();

    TagBuilder Table();

    TableBuilder<T> Table<T>(IEnumerable<T> items);

    TagBuilder TBody();

    TagBuilder Td(TableScope scope);

    TagBuilder TextArea(string name, string? value, bool autoFocus = false);

    TagBuilder TextInput(string name, string? value, bool autoFocus = false);

    TagBuilder TFoot();

    TagBuilder THead();

    TagBuilder Th(TableScope scope);

    TagBuilder TimeInput(string name, TimeOnly value, bool autoFocus = false);

    TagBuilder Tr();
}
