using Microsoft.AspNetCore.Routing;

namespace Xan.AspNetCore.Mvc.Routing;

public class LinkRouter(LinkGenerator linkGenerator)
{
    public string GetUriByAction(string controller, string action, object? values = null)
    {
        ArgumentNullException.ThrowIfNull(controller);
        ArgumentNullException.ThrowIfNull(action);

        string? url = linkGenerator.GetPathByAction(action, controller, values);
        if (string.IsNullOrEmpty(url))
        {
            throw new EntryPointNotFoundException($"No Entry point found for controller: {controller} and action: {action}");
        }
        return url;
    }
}
