using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Xan.AspNetCore.Mvc.Crud;

public interface ICrudModel
{
    string Title { get; }

    Task<IHtmlContent> EditorAsync(ViewContext viewContext);
}
