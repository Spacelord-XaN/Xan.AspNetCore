using Microsoft.AspNetCore.Html;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnConfigTests;

public class SetContent
{
    [Theory]
    [AutoData]
    public void FuncWithItem(string htmlContent, int index, object item)
    {
        //  Arrange
        ColumnConfig<object> sut = new();
        Func<object, IHtmlContent> getContent = X.StrictFake<Func<object, IHtmlContent>>();
        A.CallTo(() => getContent.Invoke(item)).Returns(new HtmlString(htmlContent));

        //  Act
        sut.SetContent(getContent);

        //  Assert
        sut.GetContent(index, item).Should().BeHtml(htmlContent);

        A.CallTo(() => getContent.Invoke(item)).MustHaveHappenedOnceExactly();
    }

    [Theory]
    [AutoData]
    public void FuncWithItemAndIndex(string htmlContent, int index, object item)
    {
        //  Arrange
        ColumnConfig<object> sut = new();
        Func<int, object, IHtmlContent> getContent = X.StrictFake<Func<int, object, IHtmlContent>>();
        A.CallTo(() => getContent.Invoke(index, item)).Returns(new HtmlString(htmlContent));

        //  Act
        sut.SetContent(getContent);

        //  Assert
        sut.GetContent(index, item).Should().BeHtml(htmlContent);

        A.CallTo(() => getContent.Invoke(index, item)).MustHaveHappenedOnceExactly();
    }
}
