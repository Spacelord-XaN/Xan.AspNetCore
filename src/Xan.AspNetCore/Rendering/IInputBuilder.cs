using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Xan.AspNetCore.Rendering;

public interface IInputBuilder
    : IHtmlContent
{
    AttributeDictionary Attributes { get; }

    void AddCssClass(string value);
}
