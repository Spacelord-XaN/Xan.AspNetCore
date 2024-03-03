namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class Id
     : TestBase
{
    [Fact]
    public void ShouldPrefixWithId()
    {
        //  Arrange

        //  Act
        string result = Sut.Id("MyName");

        //  Assert
        result.Should().Be("id_MyName");
    }
}
