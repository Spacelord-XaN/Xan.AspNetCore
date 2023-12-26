using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;
using Xan.AspNetCore.TestWebApp.Models.Crud;
using Xan.AspNetCore.TestWebApp.Routing;
using Xan.AspNetCore.TestWebApp.Services.Crud;

namespace Xan.AspNetCore.TestWebApp.Controllers;

public class ShipController
    : AbstractCrudController<ShipEntity, ListParameter, ShipRouter, ShipCrudService>
{
    public ShipController(ShipCrudService service, ShipRouter router, ShipCrudModelFactory modelFactory, IValidator<ShipEntity> validator)
        : base(service, router, modelFactory, validator)
    { }

    public async Task<IActionResult> Details(int id)
    {
        ShipEntity model = await Service.GetAsync(id);
        return View(model);
    }

    protected override IActionResult RedirectToOrigin(ShipEntity entity, string? origin)
    {
        if (origin == "details")
        {
            return Redirect(Router.ToDetails(entity.Id));
        }
        return base.RedirectToOrigin(entity, origin);
    }
}
