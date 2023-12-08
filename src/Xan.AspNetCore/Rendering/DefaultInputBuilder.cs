using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Encodings.Web;

namespace Xan.AspNetCore.Rendering;

public class DefaultInputBuilder
    : IInputBuilder
{
    private readonly TagBuilder _input;

    public DefaultInputBuilder(TagBuilder input)
    {
        _input = input ?? throw new ArgumentNullException(nameof(input));
    }

    public AttributeDictionary Attributes { get => _input.Attributes; } 

    public void AddCssClass(string value)
    {
        _input.AddCssClass(value);
    }

    public void WriteTo(TextWriter writer, HtmlEncoder encoder)
    {
        ArgumentNullException.ThrowIfNull(writer);
        ArgumentNullException.ThrowIfNull(encoder);

        _input.WriteTo(writer, encoder);
    }
}
