using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Encodings.Web;

namespace Xan.AspNetCore.Rendering;

public class DataListInputBuilder(IInputBuilder input, IHtmlContent dataList)
        : IInputBuilder
{
    public AttributeDictionary Attributes { get => input.Attributes; } 

    public void AddCssClass(string value)
    {
        input.AddCssClass(value);
    }

    public void WriteTo(TextWriter writer, HtmlEncoder encoder)
    {
        ArgumentNullException.ThrowIfNull(writer);
        ArgumentNullException.ThrowIfNull(encoder);

        dataList.WriteTo(writer, encoder);
    }
}
