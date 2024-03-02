using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderExtensionsTests;

public class Title
    : TestBase
{
    [Theory]
    [AutoData]
    public void String(string value)
    {
        //  Arrange
        ColumnBuilder<object> sut = CreateSut<object>();

        //  Act
        ColumnConfig<object> result = sut.Title(value).Build();

        //  Assert
        result.Title.Should().BeHtml(value);
    }
}
