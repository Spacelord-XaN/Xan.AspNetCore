using Xan.AspNetCore.Http;

namespace Xan.AspNetCore.Tests.Http.PageSizeCookieTests;

public class SetPageSize
    : TestBase
{
    [Theory]
    [AutoData]
    public void ShouldCallAppend(string key, int value)
    {
        //  Arrange
        A.CallTo(() => ResponseCookies.Append(key, value.ToString(), PageSizeCookie.Options)).DoesNothing();

        //  Act
        ResponseCookies.SetPageSize(key, value);

        //  Assert
        A.CallTo(() => ResponseCookies.Append(key, value.ToString(), PageSizeCookie.Options)).MustHaveHappenedOnceExactly();
    }
}
