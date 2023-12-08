using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace Xan.AspNetCore.Rendering;

public class DefaultHtmlFactory
    : IHtmlFactory
{
    public DefaultHtmlFactory(IStringLocalizer stringLocalizer)
    {
        Localizer = stringLocalizer ?? throw new ArgumentNullException(nameof(stringLocalizer));
    }

    public IStringLocalizer Localizer { get; }

    public virtual IInputBuilder CheckBox(string name, bool value)
    {
        ArgumentNullException.ThrowIfNull(name);

        IInputBuilder input = Input("checkbox", name, "true");
        input.Attributes.Remove("class");
        if (value)
        {
            input.Attributes.Add("checked", null);
        }

        return input;
    }

    public virtual IInputBuilder DateInput(string name, DateOnly value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(name);

        string stringValue = value.ToString("yyyy-MM-dd");
        return DateInput(name, stringValue, autoFocus);
    }

    public virtual IInputBuilder DateInput(string name, DateTime value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(name);

        string stringValue = value.ToString("yyyy-MM-dd");
        return DateInput(name, stringValue, autoFocus);
    }

    public virtual TagBuilder Div()
        => TagBuilder("div");

    public virtual TagBuilder Heading(int level)
        => TagBuilder($"h{level}");

    public virtual IInputBuilder HiddenInput(string name, int value)
    {
        ArgumentNullException.ThrowIfNull(name);

        string stringValue = value.ToString(CultureInfo.InvariantCulture);
        return HiddenInput(name, stringValue);
    }

    public virtual IInputBuilder HiddenInput(string name, string value)
    {
        ArgumentNullException.ThrowIfNull(name);

        return Input("hidden", name, value);
    }

    public virtual string Id(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        return $"id_{name}";
    }

    public virtual IInputBuilder Input(string type, string name, string? value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(name);

        TagBuilder input = TagBuilder("input", name: name);
        input.Attributes.Add("type", type);
        if (value != null)
        {
            input.Attributes.Add("value", value);
        }
        if (autoFocus)
        {
            input.SetAutoFocus();
        }
        return new DefaultInputBuilder(input);
    }

    public virtual TagBuilder Label()
        => TagBuilder("label");

    public virtual TagBuilder Link(string url)
    {
        ArgumentNullException.ThrowIfNull(url);

        TagBuilder a = TagBuilder("a");
        a.Attributes.Add("href", url);
        return a;
    }

    public virtual IInputBuilder NumberInput(string name, int value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(name);

        string stringValue = value.ToString(CultureInfo.InvariantCulture);
        return Input("number", name, stringValue, autoFocus);
    }

    public virtual IInputBuilder NumberInput(string name, double value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(name);

        string stringValue = value.ToString(CultureInfo.InvariantCulture);
        return Input("number", name, stringValue, autoFocus);
    }

    public virtual IInputBuilder NumberInput(string name, decimal value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(name);

        string stringValue = value.ToString();
        return Input("text", name, stringValue, autoFocus);
    }

    public virtual TagBuilder Option(string? value)
    {
        TagBuilder option = TagBuilder("option");
        option.Attributes.Add("value", value ?? "null");
        return option;
    }

    public virtual TagBuilder Paragraph()
        => TagBuilder("p");

    public virtual IInputBuilder PasswordInput(string name, string? value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);

        return Input("password", name, value, autoFocus);
    }

    public virtual IInputBuilder Select(string name, string? value, SelectList items, bool submitOnChange = false, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(items);

        TagBuilder select = TagBuilder("select", name: name);
        if (autoFocus)
        {
            select.SetAutoFocus();
        }

        if (submitOnChange)
        {
            select.Attributes.Add("onchange", "this.form.submit()");
        }

        foreach (SelectListItem item in items)
        {
            bool isSelected = item.Value == value;
            TagBuilder option = SelectOption(item.Text, item.Value, isSelected);

            select.InnerHtml.AppendHtml(option);
        }

        return new DefaultInputBuilder(select);
    }

    public virtual IInputBuilder Select(string name, int? value, SelectList items, bool submitOnChange = false, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(items);

        string currentValueString = "";
        if (value.HasValue)
        {
            currentValueString = value.Value.ToString();
        }
        return Select(name, currentValueString, items, submitOnChange, autoFocus);
    }

    public virtual IInputBuilder Select<TEnum>(string name, TEnum? value, SelectList items, bool submitOnChange = false, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(items);

        string currentValueString = "";
        if (value != null)
        {
            currentValueString = value.ToString() ?? "";
        }
        return Select(name, currentValueString, items, submitOnChange, autoFocus);
    }

    public virtual TagBuilder SelectOption(string text, string? value, bool isSelected = false)
    {
        ArgumentNullException.ThrowIfNull(text);

        TagBuilder option = Option(value);
        option.InnerHtml.SetHtmlContent(text);
        if (isSelected)
        {
            option.Attributes.Add("selected", null);
        }
        return option;
    }

    public virtual TagBuilder Span()
        => TagBuilder("span");

    public virtual TagBuilder Table()
        => TagBuilder("table");

    public TableBuilder<T> Table<T>(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        return new TableBuilder<T>(items, this, Localizer);
    }

    public virtual TagBuilder TBody()
        => TagBuilder("tbody");

    public virtual TagBuilder Td(TableScope scope)
        => TableCell("td", scope);

    public virtual IInputBuilder TextArea(string name, string? value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);

        TagBuilder textArea = TagBuilder("textarea", name: name);
        if (value != null)
        {
            textArea.InnerHtml.SetContent(value);
        }
        if (autoFocus)
        {
            textArea.SetAutoFocus();
        }
        return new DefaultInputBuilder(textArea);
    }

    public virtual IInputBuilder TextInput(string name, string? value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);

        return Input("text", name, value, autoFocus);
    }

    public virtual TagBuilder TFoot()
        => TagBuilder("tfoot");

    public virtual TagBuilder THead()
        => TagBuilder("thead");

    public virtual TagBuilder Th(TableScope scope)
        => TableCell("th", scope);

    public virtual IInputBuilder TimeInput(string name, TimeOnly value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(name);

        string stringValue = value.ToString("HH:mm");
        return Input("time", name, stringValue, autoFocus);
    }

    public virtual TagBuilder Tr()
        => TagBuilder("tr");

    private IInputBuilder DateInput(string name, string stringValue, bool autoFocus)
       => Input("date", name, stringValue, autoFocus);

    private TagBuilder TableCell(string tag, TableScope scope)
    {
        TagBuilder cell = TagBuilder(tag);
        if (scope != TableScope.None)
        {
            cell.SetScope(scope.ToHtmlAttributeString());
        }
        return cell;
    }

    protected virtual TagBuilder TagBuilder(string tagName, string? name = null)
    {
        ArgumentNullException.ThrowIfNull(tagName);

        TagBuilder tag = new (tagName);
        if (name != null)
        {
            tag.SetName(name);
            tag.SetId(Id(name));
        }
        return tag;
    }
}
