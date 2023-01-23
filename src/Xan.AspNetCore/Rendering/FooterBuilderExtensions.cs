namespace Xan.AspNetCore.Rendering;

public static class FooterBuilderExtensions
{
    public static FooterBuilder For(this FooterBuilder builder, string text)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(text);

        return builder.For(text.ToHtml());
    }
}
