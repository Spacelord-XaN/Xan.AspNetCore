using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace Xan.AspNetCore.Rendering;

public interface IHtmlFactory
{
    IStringLocalizer Localizer { get; }

    IInputBuilder CheckBox(string name, bool value);

    IInputBuilder DataList(string name, string? value, ISet<string?> values, bool autoFocus = false);

    IInputBuilder DateInput(string name, DateOnly value, bool autoFocus = false);

    IInputBuilder DateInput(string name, DateTime value, bool autoFocus = false);

    IInputBuilder DateTimeInput(string name, DateTime value, bool autoFocus = false);

    TagBuilder Div();

    TagBuilder Heading(int level);

    IInputBuilder HiddenInput(string name, int value);

    IInputBuilder HiddenInput(string name, string value);

    IInputBuilder HiddenInput(string name, DateTime value);

    string Id(string name);

    IInputBuilder Input(string type, string name, string? value, bool autoFocus = false);

    TagBuilder Label();

    TagBuilder Link(string url);

    IInputBuilder NumberInput(string name, int value, bool autoFocus = false);

    IInputBuilder NumberInput(string name, double value, bool autoFocus = false);

    IInputBuilder NumberInput(string name, decimal value, bool autoFocus = false);

    TagBuilder Option(string? value);

    TagBuilder Paragraph();

    IInputBuilder PasswordInput(string name, string? value, bool autoFocus = false);

    IInputBuilder Select(string name, string? value, SelectList items, bool submitOnChange = false, bool autoFocus = false);

    IInputBuilder Select(string name, int? value, SelectList items, bool submitOnChange = false, bool autoFocus = false);

    IInputBuilder Select<TEnum>(string name, TEnum? value, SelectList items, bool submitOnChange = false, bool autoFocus = false);

    TagBuilder SelectOption(string text, string? value, bool isSelected = false);

    TagBuilder Span();

    TagBuilder Table();

    TableBuilder<T> Table<T>(IEnumerable<T> items);

    TagBuilder TBody();

    TagBuilder Td(TableScope scope);

    IInputBuilder TextArea(string name, string? value, bool autoFocus = false);

    IInputBuilder TextInput(string name, string? value, bool autoFocus = false);

    TagBuilder TFoot();

    TagBuilder THead();

    TagBuilder Th(TableScope scope);

    IInputBuilder TimeInput(string name, TimeOnly value, bool autoFocus = false);

    TagBuilder Tr();
}
