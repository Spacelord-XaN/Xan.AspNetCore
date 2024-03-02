using Microsoft.AspNetCore.Mvc;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudControllerTests;

public class Enable
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldCallEnableAndRedirectToReferrer(int id, string referrerUrl)
    {
        //  Arrange
        Sut.Request.Headers.Referer = referrerUrl;
        A.CallTo(() => Service.EnableAsync(id))
            .DoesNothing();;
            
        //  Act
        IActionResult result = await Sut.Enable(id);

        //  Assert
        using (new AssertionScope())
        {
            RedirectResult redirect = result.Should().BeOfType<RedirectResult>().Subject;
            redirect.Url.Should().Be(referrerUrl);
        }

        A.CallTo(() => Service.EnableAsync(id)).MustHaveHappenedOnceExactly();
    }
}
