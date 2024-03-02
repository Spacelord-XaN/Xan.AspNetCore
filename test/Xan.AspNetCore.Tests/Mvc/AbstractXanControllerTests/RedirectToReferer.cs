using Microsoft.AspNetCore.Mvc;

namespace Xan.AspNetCore.Tests.Mvc.AbstractXanControllerTests;

public class RedirectToReferer
{
    [Fact]
    public void NoReferrerSet_ShouldThrow()
    {
        //  Arrage
        TestController sut = new ();

        //  Act
        Action act = () => sut.RedirectToReferer();

        //  Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Theory]
    [AutoData]
    public void ReferrerSet_ShouldReturnRedirectToReferrer(string url)
    {
        //  Arrage
        TestController sut = new();
        sut.Request.Headers.Referer = url;

        //  Act
        IActionResult result = sut.RedirectToReferer();

        //  Assert
        using (new AssertionScope())
        {
            RedirectResult redirect = result.Should().BeOfType<RedirectResult>().Subject;
            redirect.Url.Should().Be(url);
        }
    }
}
