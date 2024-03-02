using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Xan.AspNetCore.Http;
using Xan.AspNetCore.Mvc.Filters;
using Xan.AspNetCore.Parameter;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Mvc.Filters.PageSizeFilterTests;

public class OnActionExecuting
{
    private readonly PageSizeFilter _sut = new ();
    private readonly DefaultHttpContext _httpContext = new();
    private readonly CookiesMock _cookies = new();

    public OnActionExecuting()
    {
        _httpContext = new DefaultHttpContext();
        _httpContext.Features.Set<IRequestCookiesFeature>(new RequestCookiesFeature(_cookies));
        _httpContext.Features.Set<IResponseCookiesFeature>(new ResponseCookiesFeatureWrapper(_cookies));
    }

    [Fact]
    public void NoParameter_ShouldDoNothing()
    {
        //  Arrange
        ActionContext actionContext = new(_httpContext, new RouteData(), new ActionDescriptor());
        ActionExecutingContext context = new(actionContext, [], new Dictionary<string, object?>(), new object());

        //  Act
        _sut.OnActionExecuting(context);

        //  Assert
        using (new AssertionScope())
        {
            _cookies.Count.Should().Be(0);
            _cookies.Keys.Should().BeEmpty();
        }
    }

    [Theory]
    [AutoData]
    public void HasParameterWithPageSize_HasPageSizeInRouteData_ShouldSetCookieToParameter(string argumentName, ListParameter parameter, int pageSizeInRoute, int pageSizeInParameter)
    {
        //  Arrange
        parameter.PageSize = pageSizeInParameter;
        Dictionary<string, object?> actionArguments = new ()
        {
            { argumentName, parameter }
        };
        RouteData routeData = new();
        routeData.Values.Add(PageSizeCookie.Key, pageSizeInRoute);
        ActionContext actionContext = new(_httpContext, routeData, new ActionDescriptor());
        ActionExecutingContext context = new(actionContext, [], actionArguments, new object());

        //  Act
        _sut.OnActionExecuting(context);

        //  Assert
        using (new AssertionScope())
        {
            _cookies.Count.Should().Be(1);
            string expectedKey = $"{PageSizeCookie.Key}.{pageSizeInRoute}";
            _cookies.Keys.Should().Contain(expectedKey);
            _cookies[expectedKey].Should().Be(parameter.PageSize.Value.ToString());
        }
    }

    [Theory]
    [AutoData]
    public void HasParameterWihtoutPageSize_HasPageSizeInRouteData_CookieIsSet_ShouldSetParameterToCookieValue(string argumentName, ListParameter parameter, int pageSizeInRoute, int pageSizeInCookies)
    {
        //  Arrange
        parameter.PageSize = null;
        Dictionary<string, object?> actionArguments = new()
        {
            { argumentName, parameter }
        };
        RouteData routeData = new();
        routeData.Values.Add(PageSizeCookie.Key, pageSizeInRoute);
        ActionContext actionContext = new(_httpContext, routeData, new ActionDescriptor());
        ActionExecutingContext context = new(actionContext, [], actionArguments, new object());
        string expectedKey = $"{PageSizeCookie.Key}.{pageSizeInRoute}";
        _cookies.SetPageSize(expectedKey, pageSizeInCookies);

        //  Act
        _sut.OnActionExecuting(context);

        //  Assert
        using (new AssertionScope())
        {
            _cookies.Count.Should().Be(1);            
            _cookies.Keys.Should().Contain(expectedKey);
            parameter.PageSize.Should().Be(pageSizeInCookies);
        }
    }

    [Theory]
    [AutoData]
    public void HasParameterWihtoutPageSize_HasPageSizeInRouteData_CookieIsNotSet_ShouldSetParameterToDefault(string argumentName, ListParameter parameter, int pageSizeInRoute, int pageSizeInCookies)
    {
        //  Arrange
        parameter.PageSize = null;
        Dictionary<string, object?> actionArguments = new()
        {
            { argumentName, parameter }
        };
        RouteData routeData = new();
        routeData.Values.Add(PageSizeCookie.Key, pageSizeInRoute);
        ActionContext actionContext = new(_httpContext, routeData, new ActionDescriptor());
        ActionExecutingContext context = new(actionContext, [], actionArguments, new object());
        string expectedKey = $"{PageSizeCookie.Key}.{pageSizeInRoute}";
        
        //  Act
        _sut.OnActionExecuting(context);

        //  Assert
        using (new AssertionScope())
        {
            _cookies.Count.Should().Be(0);
            _cookies.Keys.Should().BeEmpty();
            parameter.PageSize.Should().Be(ListParameter.DefaultPageSize);
        }
    }
}
