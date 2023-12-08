using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;
using Xan.AspNetCore.TestWebApp.Models.Crud;
using Xan.AspNetCore.TestWebApp.Routing;
using Xan.AspNetCore.TestWebApp.Services.Crud;

namespace Xan.AspNetCore.TestWebApp.Controllers;

public class ShipController
    : AbstractCrudController<ShipEntity, ListParameter>
{
    private readonly ShipCrudService _shipService;
    private readonly ICrudRouter<ShipEntity> _shipRouter;

    public ShipController(ShipCrudService service, ICrudRouter<ShipEntity> router, ShipCrudModelFactory modelFactory, IValidator<ShipEntity> validator)
        : base(service, router, modelFactory, validator)
    {
        _shipService = service ?? throw new ArgumentNullException(nameof(service));
        _shipRouter = router ?? throw new ArgumentNullException(nameof(router));
    }

    public async Task<IActionResult> Details(int id)
    {
        ShipEntity model = await _shipService.GetAsync(id);
        return View(model);
    }

    protected override IActionResult RedirectToOrigin(ShipEntity entity, string? origin)
    {
        if (origin == "details")
        {
            return Redirect(_shipRouter.ToDetails(entity.Id));
        }
        return base.RedirectToOrigin(entity, origin);
    }
}
