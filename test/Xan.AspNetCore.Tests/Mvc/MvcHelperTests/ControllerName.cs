using Xan.AspNetCore.Mvc;

namespace Xan.AspNetCore.Tests.Mvc.MvcHelperTests;

public class ControllerName
{
    private class MySuperController
    { }

    [Fact]
    public void MySuperController_ShouldRemoveControllerKeyword()
    {
        //  Arrange

        //  Act
        string result = MvcHelper.ControllerName<MySuperController>();

        //  Assert
        result.Should().Be("MySuper");
    }

    private class MySuperControllerWithOtherSuffix
    { }

    [Fact]
    public void MySuperControllerWithOtherSuffix_ShouldRemoveControllerKeyword()
    {
        //  Arrange

        //  Act
        string result = MvcHelper.ControllerName<MySuperControllerWithOtherSuffix>();

        //  Assert
        result.Should().Be("MySuperWithOtherSuffix");
    }
}
