using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Localization;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering;

public class ExtensionsTests
{
    [Fact]
    public void Int()
    {
        // Arrange
        int value = 42;

        //  Act
        IHtmlContent result = value.ToHtml();

        //  Assert
        result.Should().BeHtml("42");
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData(33, "33")]
    public void NullableInt(int? value, string expectedHtml)
    {
        // Arrange

        //  Act
        IHtmlContent result = value.ToHtml();

        //  Assert
        result.Should().BeHtml(expectedHtml);
    }

    [Theory]
    [InlineData("", "", "")]
    [InlineData("MyText", "MY Text", "MY Text")]
    public void LocalizedString(string name, string value, string expectedHtml)
    {
        // Arrange
        LocalizedString localizedString = new LocalizedString(name, value);

        //  Act
        IHtmlContent result = localizedString.ToHtml();

        //  Assert
        result.Should().BeHtml(expectedHtml);
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("asdf", "asdf")]
    public void String(string? value, string expectedHtml)
    {
        // Arrange

        //  Act
        IHtmlContent result = value.ToHtml();

        //  Assert
        result.Should().BeHtml(expectedHtml);
    }

    public static TheoryData<DateTime> ToHtmlDateData { get; } = new()
    {
        { new DateTime(2063, 04, 05, 11, 22, 33, 444) },
        { new DateTime(2063, 04, 05, 00, 00, 00, 0) },
    };

    [Theory]
    [MemberData(nameof(ToHtmlDateData))]
    public void ToHtmlDate(DateTime value)
    {
        // Arrange

        //  Act
        IHtmlContent result = value.ToHtmlDate();

        //  Assert
        result.Should().BeHtml(value.ToLongDateString());
    }

    public static TheoryData<DateTime> ToHtmlTimeStampData { get; } = new()
    {
        { new DateTime(2063, 04, 05, 11, 22, 33, 444) },
        { new DateTime(2063, 04, 05, 00, 00, 00, 0) },
    };

    [Theory]
    [MemberData(nameof(ToHtmlTimeStampData))]
    public void ToHtmlTimeStamp(DateTime value)
    {
        // Arrange

        //  Act
        IHtmlContent result = value.ToHtmlTimeStamp();

        //  Assert
        result.Should().BeHtml(value.ToString("g"));
    }

    [Fact]
    public void ToHtmlTimeStamp_Null()
    {
        // Arrange

        //  Act
        IHtmlContent result = AspNetCore.Rendering.Extensions.ToHtmlTimeStamp(null);

        //  Assert
        result.Should().BeHtml("");
    }

    [Theory]
    [AutoData]
    public void ToHtmlPrice(decimal value)
    {
        //  Arrange

        //  Act
        IHtmlContent result = value.ToHtmlPrice();

        //  Assert
        result.Should().BeHtml(value.ToString("c"));
    }

    [Fact]
    public void ToHtmlPrice_Null()
    {
        //  Arrange

        //  Act
        IHtmlContent result = AspNetCore.Rendering.Extensions.ToHtmlPrice(null);

        //  Assert
        result.Should().BeHtml("");
    }
}
