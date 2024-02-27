using Microsoft.AspNetCore.Http;

namespace Xan.AspNetCore.Tests.Http.PageSizeCookieTests;

public class TestBase
{
    public IResponseCookies ResponseCookies { get; } = X.StrictFake<IResponseCookies>();

    public IRequestCookieCollection RequestCookies { get; } = X.StrictFake<IRequestCookieCollection>();
}
