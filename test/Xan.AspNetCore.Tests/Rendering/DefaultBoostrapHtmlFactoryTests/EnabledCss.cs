namespace Xan.AspNetCore.Tests.Rendering.DefaultBoostrapHtmlFactoryTests;

public class EnabledCss
    : TestBase
{
    [Theory]
    [InlineData(false, "disabled")]
    [InlineData(true, "")]
    public void ShouldReturnCorrectClass(bool isEnabled, string expectedResult)
    {
        //  Arrange

        //  Act
        string result = Sut.EnabledCss(isEnabled);

        //  Assert
        result.Should().Be(expectedResult);
    }
}
