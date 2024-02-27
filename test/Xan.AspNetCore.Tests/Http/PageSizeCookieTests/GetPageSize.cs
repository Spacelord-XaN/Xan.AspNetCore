using Xan.AspNetCore.Http;
using Xan.AspNetCore.Parameter;

namespace Xan.AspNetCore.Tests.Http.PageSizeCookieTests;

public class GetPageSize
    : TestBase
{
    [Theory]
    [AutoData]
    public void NoCookie_ShouldReturnDefault(string key)
    {
        //  Arrange
        A.CallTo(() => RequestCookies[key]).Returns(null);

        //  Act
        int result = RequestCookies.GetPageSize(key);

        //  Assert
        result.Should().Be(ListParameter.DefaultPageSize);

        A.CallTo(() => RequestCookies[key]).MustHaveHappenedOnceExactly();
    }

    [Theory]
    [AutoData]
    public void NoValidInt_ShouldReturnDefault(string key, string value)
    {
        //  Arrange
        A.CallTo(() => RequestCookies[key]).Returns(value);

        //  Act
        int result = RequestCookies.GetPageSize(key);

        //  Assert
        result.Should().Be(ListParameter.DefaultPageSize);

        A.CallTo(() => RequestCookies[key]).MustHaveHappenedOnceExactly();
    }

    [Theory]
    [AutoData]
    public void ValidInt_ShouldReturnValue(string key, int value)
    {
        //  Arrange
        A.CallTo(() => RequestCookies[key]).Returns(value.ToString());

        //  Act
        int result = RequestCookies.GetPageSize(key);

        //  Assert
        result.Should().Be(value);

        A.CallTo(() => RequestCookies[key]).MustHaveHappenedOnceExactly();
    }
}
