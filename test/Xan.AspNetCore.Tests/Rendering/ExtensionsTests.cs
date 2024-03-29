﻿using Microsoft.AspNetCore.Html;
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
        IHtmlContent result = value.ToHtmlDisplay();

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
        IHtmlContent result = value.ToHtmlDisplay();

        //  Assert
        result.Should().BeHtml(expectedHtml);
    }
    
    [Fact]
    public void Double()
    {
        // Arrange
        double value = -42.123;

        //  Act
        IHtmlContent result = value.ToHtmlDisplay();

        //  Assert
        result.Should().BeHtml(value.ToString());
    }

    [Fact]
    public void NullableDouble()
    {
        // Arrange
        double? value = null;

        //  Act
        IHtmlContent result = value.ToHtmlDisplay();

        //  Assert
        result.Should().BeHtml("");
    }

    [Fact]
    public void Decimal()
    {
        // Arrange
        decimal value = -42.123M;

        //  Act
        IHtmlContent result = value.ToHtmlDisplay();

        //  Assert
        result.Should().BeHtml(value.ToString());
    }

    [Fact]
    public void NullableDecimal()
    {
        // Arrange
        decimal? value = null;

        //  Act
        IHtmlContent result = value.ToHtmlDisplay();

        //  Assert
        result.Should().BeHtml("");
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("asdf", "asdf")]
    public void String(string? value, string expectedHtml)
    {
        // Arrange

        //  Act
        IHtmlContent result = value.ToHtmlDisplay();

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

    public static TheoryData<DateTime> ToHtmlDateData { get; } = new()
    {
        { new DateTime(2063, 04, 05, 11, 22, 33, 444) },
        { new DateTime(2063, 04, 05, 00, 00, 00, 0) },
    };

    [Theory]
    [MemberData(nameof(ToHtmlDateData))]
    public void ToHtmlDateDisplay(DateTime value)
    {
        // Arrange

        //  Act
        IHtmlContent result = value.ToHtmlDateDisplay();

        //  Assert
        result.Should().BeHtml(value.ToLongDateString());
    }

    [Fact]
    public void ToHtmlDateDisplay_Null()
    {
        // Arrange

        //  Act
        IHtmlContent result = AspNetCore.Rendering.Extensions.ToHtmlDateDisplay(null);

        //  Assert
        result.Should().BeHtml("");
    }

    public static TheoryData<DateTime> ToHtmlTimeStampData { get; } = new()
    {
        { new DateTime(2063, 04, 05, 11, 22, 33, 444) },
        { new DateTime(2063, 04, 05, 00, 00, 00, 0) },
    };

    [Theory]
    [MemberData(nameof(ToHtmlTimeStampData))]
    public void ToHtmlTimeStampDisplay(DateTime value)
    {
        // Arrange

        //  Act
        IHtmlContent result = value.ToHtmlTimeStampDisplay();

        //  Assert
        result.Should().BeHtml(value.ToString("g"));
    }

    [Fact]
    public void ToHtmlTimeStampDisplay_Null()
    {
        // Arrange

        //  Act
        IHtmlContent result = AspNetCore.Rendering.Extensions.ToHtmlTimeStampDisplay(null);

        //  Assert
        result.Should().BeHtml("");
    }

    [Theory]
    [AutoData]
    public void ToHtmlPriceDisplay(decimal value)
    {
        //  Arrange

        //  Act
        IHtmlContent result = value.ToHtmlPriceDisplay();

        //  Assert
        result.Should().BeHtml(value.ToString("c"));
    }

    [Fact]
    public void ToHtmlPriceDisplay_Null()
    {
        //  Arrange

        //  Act
        IHtmlContent result = AspNetCore.Rendering.Extensions.ToHtmlPriceDisplay(null);

        //  Assert
        result.Should().BeHtml("");
    }
}
