using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderTests;

public class TestBase
{
    private readonly IStringLocalizer _localizer = new StringLocalizerMock();

    public TestBase()
    {
        IHtmlFactory html = new DefaultHtmlFactory(_localizer);
        Sut = new ColumnBuilder<int>(html, _localizer);
    }

    public ColumnBuilder<int> Sut { get; }
}
