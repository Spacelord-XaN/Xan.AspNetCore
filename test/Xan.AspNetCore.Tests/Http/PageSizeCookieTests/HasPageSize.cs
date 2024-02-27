using Xan.AspNetCore.Http;

namespace Xan.AspNetCore.Tests.Http.PageSizeCookieTests;

public class HasPageSize
    : TestBase
{
    [Theory]
    [AutoData]
    public void NoCookie_ShouldReturnFalse(string key)
    {
        //  Arrange
        A.CallTo(() => RequestCookies.ContainsKey(key)).Returns(false);

        //  Act
        bool result = RequestCookies.HasPageSize(key);

        //  Assert
        result.Should().BeFalse();

        A.CallTo(() => RequestCookies.ContainsKey(key)).MustHaveHappenedOnceExactly();
    }
    [Theory]
    [AutoData]
    public void CookieIsSet_ShouldReturnFalse(string key)
    {
        //  Arrange
        A.CallTo(() => RequestCookies.ContainsKey(key)).Returns(true);

        //  Act
        bool result = RequestCookies.HasPageSize(key);

        //  Assert
        result.Should().BeTrue();

        A.CallTo(() => RequestCookies.ContainsKey(key)).MustHaveHappenedOnceExactly();
    }
}
