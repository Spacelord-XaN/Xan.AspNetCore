using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderExtensionsTests;

public class ForPrice
    : TestBase
{
    [Theory]
    [AutoData]
    public void Decimal(int index, object item, decimal value)
    {
        //  Arrange
        ColumnBuilder<object> sut = CreateSut<object>();

        //  Act
        ColumnConfig<object> result = sut.ForPrice(_ => value).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml(value.ToHtmlPriceDisplay());
    }
}
