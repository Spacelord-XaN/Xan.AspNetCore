using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Models;

namespace Xan.AspNetCore.Rendering;

public class AbstractSelectListService
{
    public AbstractSelectListService(IStringLocalizer localizer)
    {
        Localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
    }
    
    public IStringLocalizer Localizer { get; }

    public SelectList States(bool includeAll = false)
        => EnumSelectList<ObjectState>(includeAll, XanAspNetCoreTexts.Singluar);

    protected SelectList EnumSelectList<TEnum>(bool includeAll, Func<TEnum?, string> getDisplayText)
        where TEnum : struct, Enum
    {
        List<Tuple<TEnum?, string>> items = new();
        if (includeAll)
        {
            LocalizedString text = Localizer[getDisplayText(null)];
            items.Add(new Tuple<TEnum?, string>(null, text));
        }

        foreach (TEnum value in Enum.GetValues<TEnum>())
        {
            LocalizedString text = Localizer[getDisplayText(value)];
            items.Add(new Tuple<TEnum?, string>(value, text));
        }

        return new SelectList(items, "Item1", "Item2");
    }
}
