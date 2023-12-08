using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.TestWebApp.Rendering;

public class TestAppHtmlFactory
    : DefaultBoostrapHtmlFactory
{
    public TestAppHtmlFactory(IStringLocalizer<SharedResources> localizer)
        : base(localizer)
    { }
}
