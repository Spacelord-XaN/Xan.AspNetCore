using Microsoft.AspNetCore.Html;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderTests;

public class Title
    : TestBase
{
    [Theory]
    [AutoData]
    public void ShouldSetAlign(string titleText)
    {
        //  Arrange        

        //  Act
        ColumnConfig<int> result = Sut.Title(new HtmlString(titleText)).Build();

        //  Assert
        result.Title.Should().BeHtml(titleText);
    }
}
