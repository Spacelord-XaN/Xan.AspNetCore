using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace Xan.AspNetCore.Rendering;

public class DefaultHtmlFactory
    : IHtmlFactory
{
    public virtual TagBuilder CheckBox(string name, bool value)
    {
        ArgumentNullException.ThrowIfNull(name);

        TagBuilder input = Input("checkbox", name, "true");
        input.Attributes.Remove("class");
        if (value)
        {
            input.Attributes.Add("checked", null);
        }

        return input;
    }

    public virtual TagBuilder DateInput(string name, DateTime value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(name);

        string stringValue = value.ToString("yyyy-MM-dd");
        return Input("date", name, stringValue, autoFocus);
    }

    public virtual TagBuilder Div()
        => TagBuilder("div");

    public virtual TagBuilder Heading(int level)
        => TagBuilder($"h{level}");

    public virtual TagBuilder HiddenInput(string name, int value)
    {
        ArgumentNullException.ThrowIfNull(name);

        string stringValue = value.ToString(CultureInfo.InvariantCulture);
        return HiddenInput(name, stringValue);
    }

    public virtual TagBuilder HiddenInput(string name, string value)
    {
        ArgumentNullException.ThrowIfNull(name);

        return Input("hidden", name, value);
    }

    public virtual string Id(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        return $"id_{name}";
    }

    public virtual TagBuilder Input(string type, string name, string? value, bool autoFocus = false)
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
        return input;
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

    public virtual TagBuilder NumberInput(string name, int value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(name);

        string stringValue = value.ToString(CultureInfo.InvariantCulture);
        return Input("number", name, stringValue, autoFocus);
    }

    public virtual TagBuilder NumberInput(string name, double value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(name);

        string stringValue = value.ToString(CultureInfo.InvariantCulture);
        return Input("number", name, stringValue, autoFocus);
    }

    public virtual TagBuilder NumberInput(string name, decimal value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(name);

        string stringValue = value.ToString();
        return Input("text", name, stringValue, autoFocus);
    }

    public virtual TagBuilder Paragraph()
        => TagBuilder("p");

    public virtual TagBuilder PasswordInput(string name, string? value, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);

        return Input("password", name, value, autoFocus);
    }

    public virtual TagBuilder Select(string name, string? value, SelectList items, bool submitOnChange = false, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(value);

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

        return select;
    }

    public virtual TagBuilder Select(string name, int? value, SelectList items, bool submitOnChange = false, bool autoFocus = false)
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

    public virtual TagBuilder Select<TEnum>(string name, TEnum? value, SelectList items, bool submitOnChange = false, bool autoFocus = false)
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

        TagBuilder option = TagBuilder("option");
        option.Attributes.Add("value", value ?? "null");
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

    public virtual TagBuilder TBody()
        => TagBuilder("tbody");

    public virtual TagBuilder Td(TableScope scope)
        => TableCell("td", scope);

    public virtual TagBuilder TextArea(string name, string? value, bool autoFocus = false)
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
        return textArea;
    }

    public virtual TagBuilder TextInput(string name, string? value, bool autoFocus = false)
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

    public virtual TagBuilder Tr()
        => TagBuilder("tr");

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
