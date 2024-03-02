using Microsoft.AspNetCore.Mvc;

namespace Xan.AspNetCore.Tests.Mvc.Crud.AbstractCrudControllerTests;

public class Delete
    : TestBase
{
    [Theory]
    [AutoData]
    public async Task ShouldCallDeleteAndRedirectToReferrer(int id, string referrerUrl)
    {
        //  Arrange
        Sut.Request.Headers.Referer = referrerUrl;
        A.CallTo(() => Service.DeleteAsync(id))
            .DoesNothing();;
            
        //  Act
        IActionResult result = await Sut.Delete(id);

        //  Assert
        using (new AssertionScope())
        {
            RedirectResult redirect = result.Should().BeOfType<RedirectResult>().Subject;
            redirect.Url.Should().Be(referrerUrl);
        }

        A.CallTo(() => Service.DeleteAsync(id)).MustHaveHappenedOnceExactly();
    }
}
