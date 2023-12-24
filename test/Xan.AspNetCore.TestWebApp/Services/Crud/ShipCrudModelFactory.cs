using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;
using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.TestWebApp.Models.Crud;
using Xan.AspNetCore.TestWebApp.Rendering;
using Xan.AspNetCore.TestWebApp.Routing;
using Xan.Extensions.Collections.Generic;

namespace Xan.AspNetCore.TestWebApp.Services.Crud;

public class ShipCrudModelFactory
    : AbstractCrudModelFactory<ShipEntity, ListParameter, ShipRouter>
{
    private readonly TestAppHtmlFactory _html;

    public ShipCrudModelFactory(TestAppHtmlFactory html, ShipRouter router)
        : base(router)
    {
        _html = html ?? throw new ArgumentNullException(nameof(html));
    }

    protected override string CreateTitle => "Create ship";

    protected override string EditTitle => "Edit ship";

    protected override string ListTitle => "Ships";

    protected override IHtmlContent CreateEditor(ViewContext viewContext, ShipEntity entity)
    {
        ArgumentNullException.ThrowIfNull(viewContext);
        ArgumentNullException.ThrowIfNull(entity);

        HtmlContentBuilder result = new();
        result.AppendHtml(_html.HiddenInput(nameof(entity.Id), entity.Id));
        result.AppendHtml(_html.TextInputField(nameof(entity.Name), entity.Name, "Name", autoFocus: true));
        result.AppendHtml(_html.NumberInputField(nameof(entity.LengthInMeters), entity.LengthInMeters, "Length [m]"));
        return result;
    }

    protected override IHtmlContent CreateTable(ViewContext viewContext, IPaginatedList<CrudItemModel<ShipEntity>> model)
    {
        ArgumentNullException.ThrowIfNull(viewContext);
        ArgumentNullException.ThrowIfNull(model);

        return _html.Table(model)
            .IdColumn()
            .Column(c => c.PercentWidth(80).BreakText().Title("Name").For(item => item.Entity.Name))
            .Column(c => c.PercentWidth(20).Title("Length [m]").For(item => item.Entity.LengthInMeters))
            .CreatedAtColumn()
            .UpdatedAtColumn()
            .StateColumn()
            .Column(c => c.ForLink(item => Router.ToDetails(item.Entity.Id), "Details"))
            .EditLinkColumn(Router)
            .DeleteOrToggleStateLinkColumn(Router)
            .Build();
    }
}
