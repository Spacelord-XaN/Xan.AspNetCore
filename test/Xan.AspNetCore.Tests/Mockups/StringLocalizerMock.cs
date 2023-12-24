using Microsoft.Extensions.Localization;

namespace Xan.AspNetCore.Tests.Mockups;

public class StringLocalizerMock
    : IStringLocalizer
{
    public LocalizedString this[string name]
    {
        get
        {
            ArgumentNullException.ThrowIfNull(name);
            return new(name, name);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            ArgumentNullException.ThrowIfNull(name);
            ArgumentNullException.ThrowIfNull(arguments);

            return new(name, string.Format(name, arguments));
        }

    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        => Enumerable.Empty<LocalizedString>();
}
