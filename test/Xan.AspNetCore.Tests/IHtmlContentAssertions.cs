using FluentAssertions.Primitives;
using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;

namespace Xan.AspNetCore.Tests;

public static class IHtmlContentExtensions
{
    public static IHtmlContentAssertions Should(this IHtmlContent instance)
        => new(instance);
}

public class IHtmlContentAssertions
    : ReferenceTypeAssertions<IHtmlContent, IHtmlContentAssertions>
{
    public IHtmlContentAssertions(IHtmlContent instance)
        : base(instance)
    { }

    protected override string Identifier => "IHtmlContent";

    public AndConstraint<IHtmlContentAssertions> Html(string expectedHtml)
    {
        using StringWriter writer = new();
        Subject.WriteTo(writer, HtmlEncoder.Default);

        string actualHtml = writer.ToString();
        actualHtml.Should().Be(expectedHtml);

        return new(this);
    }
}