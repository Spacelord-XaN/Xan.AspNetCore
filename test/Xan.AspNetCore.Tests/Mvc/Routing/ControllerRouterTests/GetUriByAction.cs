using Xan.AspNetCore.Mvc.Routing;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Mvc.Routing.ControllerRouterTests;

public class GetUriByAction
{
    private readonly LinkGeneratorMock _linkGenerator = new();

    [Fact]
    public void ShouldReturnCorrectUrl()
    {
        //  Arrange
        ControllerRouter router = new ("MyController", _linkGenerator);

        //  Act
        string result = router.GetUriByAction("MyAction", new { id = 666, name = "hello world" });

        //  Assert
        Assert.Equal("//id=666&name=hello world&action=MyAction&controller=MyController", result);
    }
}
