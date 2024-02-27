using Microsoft.AspNetCore.Routing;
using Xan.AspNetCore.Http;

namespace Xan.AspNetCore.Tests.Http.PageSizeCookieTests;

public class FindKey
{
    [Fact]
    public void Empty_ShouldReturnKeyOnly()
    {
        //  Arrange
        RouteValueDictionary routeValues = [];

        //  Act
        string result = PageSizeCookie.FindKey(routeValues);

        //  Assert
        result.Should().Be(PageSizeCookie.Key);
    }

    
    [Theory]
    [AutoData]
    public void NotEmptyAndNotSet_ShouldReturnKeyAndValues(string key, string value)
    {
        //  Arrange
        RouteValueDictionary routeValues = [];
        routeValues.Add(key, value);

        //  Act
        string result = PageSizeCookie.FindKey(routeValues);

        //  Assert
        result.Should().Be(PageSizeCookie.Key + "." + value);
    }
}
