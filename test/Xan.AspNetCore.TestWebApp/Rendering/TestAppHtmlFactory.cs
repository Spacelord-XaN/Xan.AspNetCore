using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.TestWebApp.Models.Crud;

namespace Xan.AspNetCore.TestWebApp.Rendering;

public class TestAppHtmlFactory(IStringLocalizer<SharedResources> localizer)
    : DefaultBoostrapHtmlFactory(localizer)
{
    public HtmlContentBuilder HiddenInputs(AbstractCrudEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        HtmlContentBuilder builder = new();
        builder.AppendHtml(this.HiddenIEntityInput(entity));
        builder.AppendHtml(this.HiddenIHasTimestampsInput(entity));
        return builder;
    }
}
