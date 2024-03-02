using Xan.AspNetCore.Mvc.Routing;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Mvc.Routing.LinkRouterTests;

public class GetUriByAction
{
    private readonly LinkGeneratorMock _linkGenerator = new();

    [Fact]
    public void ShouldReturnCorrectUrl()
    {
        //  Arrange
        LinkRouter router = new (_linkGenerator);

        //  Act
        string result = router.GetUriByAction("MyController2", "MyAction2", new { id = 777, name = "glipp glapp" });

        //  Assert
        Assert.Equal("//id=777&name=glipp glapp&action=MyAction2&controller=MyController2", result);
    }
}
