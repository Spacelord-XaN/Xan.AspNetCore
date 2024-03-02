using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderExtensionsTests;

public class TestBase
{
    protected static ColumnBuilder<T> CreateSut<T>()
    {
        IStringLocalizer localizer = new StringLocalizerMock();
        IHtmlFactory html = new DefaultHtmlFactory(localizer);
        return new ColumnBuilder<T>(html, localizer);
    }
}
