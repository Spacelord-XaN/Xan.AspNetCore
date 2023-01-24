using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections;

namespace Xan.AspNetCore.Mvc.Crud;

public interface ICrudListModel
    : IPaginatedList
{
    ICrudRouter Router { get; }

    ListParameter Parameter { get; }    

    string ListTitle { get; }

    string CreateText { get; }

    Task<IHtmlContent> TableAsync(ViewContext viewContext);
}
