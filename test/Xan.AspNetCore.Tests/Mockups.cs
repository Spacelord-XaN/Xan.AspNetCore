using Microsoft.Extensions.Localization;
using Moq;

namespace Xan.AspNetCore.Tests;

public static class Mockups
{
    public static IStringLocalizer StringLocalizer()
        => new Mock<IStringLocalizer>().Object;
}
