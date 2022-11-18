using Microsoft.AspNetCore.Routing;

namespace Xan.AspNetCore.Mvc.Routing;

public class ControllerRouter
    : LinkRouter
{
    public ControllerRouter(string controller, LinkGenerator linkGenerator)
        : base(linkGenerator)
    {
        Controller = controller ?? throw new ArgumentNullException(nameof(controller));
    }

    protected string Controller { get; }

    public string GetUriByAction(string action, object? values = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        return GetUriByAction(Controller, action, values);
    }
}
