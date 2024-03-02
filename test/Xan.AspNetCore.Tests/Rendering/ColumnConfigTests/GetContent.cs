using Microsoft.AspNetCore.Html;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnConfigTests;

public class GetContent
{
    [Theory]
    [AutoData]
    public void NoGetterProvided_ShouldThrowException(int index, object item)
    {
        //  Arrange
        ColumnConfig<object> sut = new();
        
        //  Act
        Action act = () => sut.GetContent(index, item);

        //  Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [AutoData]
    public void GetterProvidedShouldCallIt(string htmlContent, int index, object item)
    {
        //  Arrange
        ColumnConfig<object> sut = new();
        Func<int, object, IHtmlContent> getContent = X.StrictFake<Func<int, object, IHtmlContent>>();
        A.CallTo(() => getContent.Invoke(index, item)).Returns(new HtmlString(htmlContent));
        sut.SetContent(getContent);

        //  Act
        IHtmlContent content = sut.GetContent(index, item);

        //  Assert
        content.Should().BeHtml(htmlContent);

        A.CallTo(() => getContent.Invoke(index, item)).MustHaveHappenedOnceExactly();
    }
}
