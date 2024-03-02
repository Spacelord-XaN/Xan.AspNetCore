using Microsoft.AspNetCore.Mvc;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudControllerTests;

public class Disable
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldCallDisableAndRedirectToReferrer(int id, string referrerUrl)
    {
        //  Arrange
        Sut.Request.Headers.Referer = referrerUrl;
        A.CallTo(() => Service.DisableAsync(id))
            .DoesNothing();;
            
        //  Act
        IActionResult result = await Sut.Disable(id);

        //  Assert
        using (new AssertionScope())
        {
            RedirectResult redirect = result.Should().BeOfType<RedirectResult>().Subject;
            redirect.Url.Should().Be(referrerUrl);
        }

        A.CallTo(() => Service.DisableAsync(id)).MustHaveHappenedOnceExactly();
    }
}
