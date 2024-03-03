using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Mvc.ViewFeatures;

namespace Xan.AspNetCore.Tests.Rendering.DefaultBoostrapHtmlFactoryTests;

public class TitleCardHeader
    : TestBase
{
    [Theory]
    [AutoData]
    public void TitleSet_ShouldReturnEmpty(string name, string value)
    {
        //  Arrange
        ViewContext.ViewData.SetTitle(new LocalizedString(name, value));

        //  Act
        IHtmlContent reuslt = Sut.TitleCardHeader();

        //  Assert
        reuslt.Should().BeHtml($"""<h4 class="card-header">{value}</h4>""");
    }
}
