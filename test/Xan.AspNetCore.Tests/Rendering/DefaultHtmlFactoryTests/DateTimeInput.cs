using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class DateTimeInput
    : TestBase
{

    [Fact]
    public void DateTime()
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.DateTimeInput("BirthDate", new DateTime(1234, 12, 31, 11, 22, 33));

        //  Assert
        result.Should().BeHtml("""<input id="id_BirthDate" name="BirthDate" type="datetime-local" value="1234-12-31T11:22"></input>""");
    }
}
