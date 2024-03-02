using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Xan.AspNetCore.Mvc.Filters;

namespace Xan.AspNetCore.Tests.Mvc.Filters.ActionExecutingContextExtensionsTests;

public class FirstOrDefaultArgument
{
    [Fact]
    public void Empty_ShouldReturnNull()
    {
        //  Arrange
        ActionContext actionContext = new(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
        ActionExecutingContext context = new (actionContext, [], new Dictionary<string, object?>(), new object());

        //  Actt
        string? result = context.FirstOrDefaultArgument<string>();

        //  Assert
        result.Should().BeNull();
    }

    [Theory]
    [AutoData]
    public void ShouldReturnFirstStringArgument(int int1, int int2, string s1, string s2)
    {
        //  Arrange
        Dictionary<string, object?> actionArguments = new ()
        {
            { nameof(int1), int1 },
            { nameof(s1), s1 },
            { nameof(int2), int2 },
            { nameof(s2), s2 },
        };

        ActionContext actionContext = new(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
        ActionExecutingContext context = new(actionContext, [], actionArguments, new object());

        //  Actt
        string? result = context.FirstOrDefaultArgument<string>();

        //  Assert
        result.Should().Be(s1);
    }

    [Theory]
    [AutoData]
    public void NoString_ShouldReturnNull(int int1, int int2)
    {
        //  Arrange
        Dictionary<string, object?> actionArguments = new ()
        {
            { nameof(int1), int1 },
            { nameof(int2), int2 },
        };

        ActionContext actionContext = new(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
        ActionExecutingContext context = new(actionContext, [], actionArguments, new object());

        //  Actt
        string? result = context.FirstOrDefaultArgument<string>();

        //  Assert
        result.Should().BeNull();
    }
}
