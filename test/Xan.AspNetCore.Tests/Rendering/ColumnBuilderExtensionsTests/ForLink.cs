using Microsoft.AspNetCore.Html;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderExtensionsTests;

public class ForLink
    : TestBase
{
    private readonly ICrudRouter _router = X.StrictFake<ICrudRouter>();

    [Theory]
    [AutoData]
    public void GetUrlFunc_StringText(int index, object item, string url, string text)
    {
        //  Arrange
        ColumnBuilder<object> Sut = CreateSut<object>();

        //  Act
        ColumnConfig<object> result = Sut.ForLink(_ => url, text).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml($"""<a href="{url}">{text}</a>""");
    }

    [Theory]
    [AutoData]
    public void GetUrlFunc_GetTextFunc(int index, object item, string url, string text)
    {
        //  Arrange
        ColumnBuilder<object> Sut = CreateSut<object>();

        //  Act
        ColumnConfig<object> result = Sut.ForLink(_ => url, _ => text).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml($"""<a href="{url}">{text}</a>""");
    }

    [Theory]
    [AutoData]
    public void GetUrlFunc_GetHtmlContentFunc(int index, object item, string url, string text)
    {
        //  Arrange
        ColumnBuilder<object> Sut = CreateSut<object>();

        //  Act
        ColumnConfig<object> result = Sut.ForLink(_ => url, _ => new HtmlString(text)).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml($"""<a href="{url}">{text}</a>""");
    }

    [Theory]
    [AutoData]
    public void GetUrlFunc_StringText_IsVisibleFunc_NotVisible(int index, object item, string url, string text)
    {
        //  Arrange
        ColumnBuilder<object> Sut = CreateSut<object>();

        //  Act
        ColumnConfig<object> result = Sut.ForLink(_ => url, text, _ => false).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml("");
    }

    [Theory]
    [AutoData]
    public void GetUrlFunc_StringText_IsVisibleFunc_Visible(int index, object item, string url, string text)
    {
        //  Arrange
        ColumnBuilder<object> Sut = CreateSut<object>();

        //  Act
        ColumnConfig<object> result = Sut.ForLink(_ => url, text, _ => true).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml($"""<a href="{url}">{text}</a>""");
    }

    [Theory]
    [AutoData]
    public void GetUrlFunc_GetHtmlContentFunc_IsVisibleFunc_NotVisible(int index, object item, string url, string text)
    {
        //  Arrange
        ColumnBuilder<object> Sut = CreateSut<object>();

        //  Act
        ColumnConfig<object> result = Sut.ForLink(_ => url, _ => new HtmlString(text), _ => false).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml("");
    }

    [Theory]
    [AutoData]
    public void GetUrlFunc_GetHtmlContentFunc_IsVisibleFunc_Visible(int index, object item, string url, string text)
    {
        //  Arrange
        ColumnBuilder<object> Sut = CreateSut<object>();

        //  Act
        ColumnConfig<object> result = Sut.ForLink(_ => url, _ => new HtmlString(text), _ => true).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml($"""<a href="{url}">{text}</a>""");
    }
}
