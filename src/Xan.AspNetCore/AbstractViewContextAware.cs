using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Xan.AspNetCore;

public abstract class AbstractViewContextAware
    : IViewContextAware
{
    private ViewContext? _viewContext;

    public ViewContext ViewContext
    {
        get
        {
            if (_viewContext == null)
            {
                throw new InvalidOperationException($"No {nameof(ViewContext)} set, forgot to call {Contextualize}?");
            }
            return _viewContext;
        }
    }

    public virtual void Contextualize(ViewContext viewContext)
    {
        ArgumentNullException.ThrowIfNull(viewContext);

        _viewContext = viewContext;
    }
}
