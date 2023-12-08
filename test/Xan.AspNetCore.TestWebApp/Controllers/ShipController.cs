using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Parameter;
using Xan.AspNetCore.TestWebApp.Models.Crud;
using Xan.AspNetCore.TestWebApp.Services.Crud;

namespace Xan.AspNetCore.TestWebApp.Controllers;

public class ShipController
    : AbstractCrudController<ShipEntity, ListParameter>
{
    private readonly ShipCrudService _shipService;

    public ShipController(ShipCrudService service, ICrudRouter<ShipEntity> router, ShipCrudModelFactory modelFactory, IValidator<ShipEntity> validator)
        : base(service, router, modelFactory, validator)
    {
        _shipService = service ?? throw new ArgumentNullException(nameof(service));
    }

    public async Task<IActionResult> Details(int id)
    {
        ShipEntity model = await _shipService.GetAsync(id);
        return View(model);
    }
}
