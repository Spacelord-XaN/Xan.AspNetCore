using Microsoft.AspNetCore.Routing;
using Xan.AspNetCore.Http;

namespace Xan.AspNetCore.Tests.Http.PageSizeCookieTests;

public class HasPageSize_RouteValueDictionary
    : TestBase
{
    [Fact]
    public void EmptyAndNotSet_ShouldReturnFalse()
    {
        //  Arrange
        RouteValueDictionary routeValues = [];
        A.CallTo(() => RequestCookies.ContainsKey(PageSizeCookie.Key)).Returns(false);

        //  Act
        bool result = RequestCookies.HasPageSize(routeValues);

        //  Assert
        result.Should().BeFalse();

        A.CallTo(() => RequestCookies.ContainsKey(PageSizeCookie.Key)).MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public void EmptyAndSet_ShouldReturnTrue()
    {
        //  Arrange
        RouteValueDictionary routeValues = [];
        A.CallTo(() => RequestCookies.ContainsKey(PageSizeCookie.Key)).Returns(true);

        //  Act
        bool result = RequestCookies.HasPageSize(routeValues);

        //  Assert
        result.Should().BeTrue();

        A.CallTo(() => RequestCookies.ContainsKey(PageSizeCookie.Key)).MustHaveHappenedOnceExactly();
    }
    
    [Theory]
    [AutoData]
    public void NotEmptyAndNotSet_ShouldReturnFalse(string key, string value)
    {
        //  Arrange
        RouteValueDictionary routeValues = [];
        routeValues.Add(key, value);
        A.CallTo(() => RequestCookies.ContainsKey(PageSizeCookie.Key + "." + value)).Returns(false);

        //  Act
        bool result = RequestCookies.HasPageSize(routeValues);

        //  Assert
        result.Should().BeFalse();

        A.CallTo(() => RequestCookies.ContainsKey(PageSizeCookie.Key + "." + value)).MustHaveHappenedOnceExactly();
    }

    [Theory]
    [AutoData]
    public void NotEmptyAndSet_ShouldReturnFalse(string key, string value)
    {
        //  Arrange
        RouteValueDictionary routeValues = [];
        routeValues.Add(key, value);
        A.CallTo(() => RequestCookies.ContainsKey(PageSizeCookie.Key + "." + value)).Returns(true);

        //  Act
        bool result = RequestCookies.HasPageSize(routeValues);

        //  Assert
        result.Should().BeTrue();

        A.CallTo(() => RequestCookies.ContainsKey(PageSizeCookie.Key + "." + value)).MustHaveHappenedOnceExactly();
    }
}
