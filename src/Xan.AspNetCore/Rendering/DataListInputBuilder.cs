using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Encodings.Web;

namespace Xan.AspNetCore.Rendering;

public class DataListInputBuilder
    : IInputBuilder
{
    private readonly IHtmlContent _dataList;
    private readonly IInputBuilder _input;

    public DataListInputBuilder(IInputBuilder input, IHtmlContent dataList)
    {
        _input = input ?? throw new ArgumentNullException(nameof(input));
        _dataList = dataList ?? throw new ArgumentNullException(nameof(dataList));
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

        _dataList.WriteTo(writer, encoder);
    }
}
