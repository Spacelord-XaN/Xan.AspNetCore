using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderExtensionsTests;

public class For
    : TestBase
{
    [Theory]
    [AutoData]
    public void Int(int index, object item, int value)
    {
        //  Arrange
        ColumnBuilder<object> sut = CreateSut<object>();

        //  Act
        ColumnConfig<object> result = sut.For(_ => value).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml(value.ToHtmlDisplay());
    }

    [Theory]
    [AutoData]
    public void Double(int index, object item, double value)
    {
        //  Arrange
        ColumnBuilder<object> sut = CreateSut<object>();

        //  Act
        ColumnConfig<object> result = sut.For(_ => value).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml(value.ToHtmlDisplay());
    }

    [Theory]
    [AutoData]
    public void Decimal(int index, object item, decimal value)
    {
        //  Arrange
        ColumnBuilder<object> sut = CreateSut<object>();

        //  Act
        ColumnConfig<object> result = sut.For(_ => value).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml(value.ToHtmlDisplay());
    }

    [Theory]
    [AutoData]
    public void String(int index, object item, string value)
    {
        //  Arrange
        ColumnBuilder<object> sut = CreateSut<object>();

        //  Act
        ColumnConfig<object> result = sut.For(_ => value).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml(value);
    }


    [Theory]
    [AutoData]
    public void DateTime(int index, object item, DateTime value)
    {
        //  Arrange
        ColumnBuilder<object> sut = CreateSut<object>();

        //  Act
        ColumnConfig<object> result = sut.For(_ => value).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml(value.ToHtmlTimeStampDisplay());
    }
}
