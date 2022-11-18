using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Parameter;
using Xan.Extensions.Collections;

namespace Xan.AspNetCore.Mvc.Crud;

public interface ICrudListModel
    : IPaginatedList
{
    ICrudRouter Router { get; }

    ListParameter Parameter { get; }    

    LocalizedString ListTitle { get; }

    LocalizedString CreateText { get; }

    Task<IHtmlContent> TableAsync(ViewContext viewContext);
}
