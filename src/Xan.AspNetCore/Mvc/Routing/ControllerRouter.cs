using Microsoft.AspNetCore.Routing;

namespace Xan.AspNetCore.Mvc.Routing;

public class ControllerRouter(string controller, LinkGenerator linkGenerator)
    : LinkRouter(linkGenerator)
{
    protected string Controller { get; } = controller ?? throw new ArgumentNullException(nameof(controller));

    public string GetUriByAction(string action, object? values = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        return GetUriByAction(Controller, action, values);
    }
}
