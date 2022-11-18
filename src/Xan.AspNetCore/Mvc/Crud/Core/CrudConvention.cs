using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Xan.AspNetCore.Mvc.Crud.Core;

internal sealed class CrudConvention
    : IApplicationModelConvention
{
    private readonly IReadOnlyList<ControllerModel> _controllerModels;

    public CrudConvention(IReadOnlyList<ControllerModel> controllerModels)
    {
        _controllerModels = controllerModels ?? throw new ArgumentNullException(nameof(controllerModels));
    }

    public void Apply(ApplicationModel application)
    {
        ArgumentNullException.ThrowIfNull(application);

        foreach (ControllerModel controllerModel in _controllerModels)
        {
            application.Controllers.Add(controllerModel);
        }
    }
}
