using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace Xan.AspNetCore.Mvc.Crud;

public interface ICrudModel
{
    LocalizedString Title { get; }

    Task<IHtmlContent> EditorAsync(ViewContext viewContext);
}
