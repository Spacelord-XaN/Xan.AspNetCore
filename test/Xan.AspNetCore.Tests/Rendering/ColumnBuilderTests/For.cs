using Microsoft.AspNetCore.Html;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderTests;

public class For
    : TestBase
{
    [Theory]
    [AutoData]
    public void OnlyWithItem_ShouldSetGetContent(int index, int item, string htmlString)
    {
        //  Arrange
        Func<int, IHtmlContent> getContent = X.StrictFake<Func<int, IHtmlContent>>();
        A.CallTo(() => getContent.Invoke(item)).Returns(new HtmlString(htmlString));

        //  Act
        ColumnConfig<int> result = Sut.For(getContent).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml(htmlString);

        A.CallTo(() => getContent.Invoke(item)).MustHaveHappenedOnceExactly();
    }

    [Theory]
    [AutoData]
    public void WithItemAndIndex_ShouldSetGetContent(int index, int item, string htmlString)
    {
        //  Arrange
        Func<int, int, IHtmlContent> getContent = X.StrictFake<Func<int, int, IHtmlContent>>();
        A.CallTo(() => getContent.Invoke(index, item)).Returns(new HtmlString(htmlString));

        //  Act
        ColumnConfig<int> result = Sut.For(getContent).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml(htmlString);

        A.CallTo(() => getContent.Invoke(index, item)).MustHaveHappenedOnceExactly();
    }
}
