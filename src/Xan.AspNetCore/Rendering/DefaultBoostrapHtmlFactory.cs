using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Mvc.ViewFeatures;

namespace Xan.AspNetCore.Rendering;

public class DefaultBoostrapHtmlFactory(IStringLocalizer localizer)
    : DefaultHtmlFactory(localizer)
    , IBootstrapHtmlFactory
{
    private ViewContext? _viewContext;

    public virtual void Contextualize(ViewContext viewContext)
    {
        ArgumentNullException.ThrowIfNull(viewContext);

        _viewContext = viewContext;
    }

    public ViewContext ViewContext
    {
        get
        {
            if (_viewContext == null)
            {
                throw new InvalidOperationException($"No {nameof(ViewContext)} set, forgot to call {Contextualize}?");
            }
            return _viewContext;
        }
    }

    public TagBuilder CheckBoxField(string name, bool value, string title)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(title);

        HtmlContentBuilder content = new();

        IInputBuilder input = CheckBox(name, value);
        input.AddCssClass("form-check-input");
        content.AppendHtml(input);

        TagBuilder label = Label();
        label.AddCssClass("form-check-label");
        label.InnerHtml.SetHtmlContent(title);
        content.AppendHtml(label);

        ModelStateEntry? stateEntry = ViewContext.ModelState[name];
        if (stateEntry != null && stateEntry.ValidationState == ModelValidationState.Invalid)
        {
            input.AddCssClass("is-invalid");

            IHtmlContent validationResult = FieldValidationResults(stateEntry);
            TagBuilder validationFeedback = Div();
            validationFeedback.AddCssClass("invalid-feedback");
            validationFeedback.InnerHtml.SetHtmlContent(validationResult);

            content.AppendHtml(validationFeedback);
        }

        TagBuilder div = Div();
        div.AddCssClass("form-check mb-3");
        div.InnerHtml.SetHtmlContent(content);
        return div;
    }

    public TagBuilder DataListField(string name, string? value, ISet<string?> values, string title, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(values);
        ArgumentNullException.ThrowIfNull(title);

        IInputBuilder input = DataList(name, value, values, autoFocus: autoFocus);
        input.AddCssClass("form-control");

        return InputField(name, input, title);
    }

    public TagBuilder DateInputField(string name, DateOnly value, string title, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(title);

        IInputBuilder input = DateInput(name, value, autoFocus);
        return DateInputField(name, input, title);
    }

    public TagBuilder DateInputField(string name, DateTime value, string title, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(title);

        IInputBuilder input = DateInput(name, value, autoFocus);
        return DateInputField(name, input, title);
    }

    public TagBuilder DateTimeInputField(string name, DateTime value, string title, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(title);

        IInputBuilder input = DateTimeInput(name, value, autoFocus);
        return DateInputField(name, input, title);
    }

    public string EnabledCss(bool isEnabled)
    {
        if (isEnabled)
        {
            return "";
        }
        return "disabled";
    }

    public TagBuilder EmailInputField(string name, string? value, string title, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(title);

        IInputBuilder input = Input("email", name, value, autoFocus);
        input.AddCssClass("form-control");

        return InputField(name, input, title);
    }

    public IHtmlContent NumberInputField(string name, int value, string title, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(title);

        IInputBuilder input = NumberInput(name, value, autoFocus);
        input.AddCssClass("form-control");

        return InputField(name, input, title);
    }

    public IHtmlContent NumberInputField(string name, double value, string title, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(title);

        IInputBuilder input = NumberInput(name, value, autoFocus);
        input.AddCssClass("form-control");

        return InputField(name, input, title);
    }

    public IHtmlContent NumberInputField(string name, decimal value, string title, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(title);

        IInputBuilder input = NumberInput(name, value, autoFocus);
        input.AddCssClass("form-control");

        return InputField(name, input, title);
    }

    public TagBuilder PasswordInputField(string name, string? value, string title, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(title);

        IInputBuilder input = PasswordInput(name, value, autoFocus);
        input.AddCssClass("form-control");

        return InputField(name, input, title);
    }

    public TagBuilder PhoneNumberInputField(string name, string? value, string title, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(title);

        IInputBuilder input = Input("tel", name, value, autoFocus);
        input.AddCssClass("form-control");

        return InputField(name, input, title);
    }

    public override IInputBuilder Select(string name, string? value, SelectList items, bool submitOnChange = false, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(items);

        IInputBuilder select = base.Select(name, value, items, submitOnChange, autoFocus);
        select.AddCssClass("form-select");
        return select;
    }

    public TagBuilder SelectField<TEnum>(string name, TEnum? value, SelectList items, string title, bool submitOnChange = false, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(title);

        IInputBuilder input = Select(name, value, items, submitOnChange, autoFocus);

        return InputField(name, input, title);
    }

    public override TagBuilder Table()
    {
        TagBuilder table = base.Table();
        table.AddCssClass("table table-striped m-0");
        return table;
    }

    public TagBuilder TextInputField(string name, string? value, string title, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(title);

        IInputBuilder input = TextInput(name, value, autoFocus);
        input.AddCssClass("form-control");

        return InputField(name, input, title);
    }

    public TagBuilder TextAreaField(string name, string? value, string title, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(title);

        IInputBuilder input = TextArea(name, value, autoFocus);
        input.AddCssClass("form-control");

        TagBuilder field = InputField(name, input, title);
        return field;
    }

    public TagBuilder TimeInputField(string name, TimeOnly value, string title, bool autoFocus = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(title);

        IInputBuilder input = TimeInput(name, value, autoFocus);
        input.AddCssClass("form-control");

        return InputField(name, input, title);
    }

    public IHtmlContent TitleCardHeader()
    {
        string? title = ViewContext.ViewData.Title();
        IHtmlContent htmlTitle;
        if (title != null)
        {
            htmlTitle = title.ToHtml();
        }
        else
        {
            htmlTitle = new HtmlString("");
        }

        TagBuilder heading = Heading(4);
        heading.AddCssClass("card-header");
        heading.InnerHtml.SetHtmlContent(htmlTitle);
        return heading;
    }

    public IHtmlContent NonFieldValidationResults()
    {
        HtmlContentBuilder errorMessages = new();
        foreach (KeyValuePair<string, ModelStateEntry> modelState in ViewContext.ModelState)
        {
            if (!string.IsNullOrEmpty(modelState.Key)
                && modelState.Value.ValidationState == ModelValidationState.Invalid)
            {
                foreach (ModelError error in modelState.Value.Errors)
                {
                    errorMessages.AppendHtmlLine(error.ErrorMessage);
                }
            }
        }

        if (errorMessages.Count == 0)
        {
            return new HtmlString(null);
        }
        return errorMessages;
    }

    protected TagBuilder DateInputField(string name, IInputBuilder input, string title)
    {
        input.AddCssClass("form-control");

        return InputField(name, input, title);
    }

    protected TagBuilder InputField(string name, IInputBuilder input, string title)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(title);

        TagBuilder div = Div();
        div.AddCssClass("mb-3");

        TagBuilder label = Label();
        label.AddCssClass("form-label");
        label.InnerHtml.SetHtmlContent(title);
        div.InnerHtml.AppendHtml(label);

        div.InnerHtml.AppendHtml(input);

        ModelStateEntry? stateEntry = ViewContext.ModelState[name];
        if (stateEntry != null && stateEntry.ValidationState == ModelValidationState.Invalid)
        {
            input.AddCssClass("is-invalid");

            IHtmlContent validationResult = FieldValidationResults(stateEntry);
            TagBuilder validationFeedback = Div();
            validationFeedback.AddCssClass("invalid-feedback");
            validationFeedback.InnerHtml.SetHtmlContent(validationResult);

            div.InnerHtml.AppendHtml(validationFeedback);
        }

        return div;
    }

    public static IHtmlContent FieldValidationResults(ModelStateEntry stateEntry)
    {
        HtmlContentBuilder errorMessages = new();
        if (stateEntry.ValidationState == ModelValidationState.Invalid)
        {
            foreach (ModelError error in stateEntry.Errors)
            {
                errorMessages.AppendHtmlLine(error.ErrorMessage);
            }
        }
        return errorMessages;
    }
}
