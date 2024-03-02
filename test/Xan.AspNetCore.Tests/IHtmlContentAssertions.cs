using FluentAssertions.Primitives;
using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;

namespace Xan.AspNetCore.Tests;

public static class IHtmlContentExtensions
{
    public static IHtmlContentAssertions Should(this IHtmlContent instance)
        => new(instance);
}

public class IHtmlContentAssertions(IHtmlContent instance)
    : ReferenceTypeAssertions<IHtmlContent, IHtmlContentAssertions>(instance)
{
    protected override string Identifier => "IHtmlContent";

    public AndConstraint<IHtmlContentAssertions> BeHtml(string expectedHtml)
    {
        using StringWriter writer = new();
        Subject.WriteTo(writer, HtmlEncoder.Default);

        string actualHtml = writer.ToString();
        actualHtml.Should().Be(expectedHtml);

        return new(this);
    }

    public AndConstraint<IHtmlContentAssertions> BeHtml(IHtmlContent content)
    {
        using StringWriter writer = new();
        content.WriteTo(writer, HtmlEncoder.Default);

        return BeHtml(writer.ToString());
    }
}