using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class DateTimeInput
{

    [Fact]
    public void DateTime()
    {
        //  Arrange
        DefaultHtmlFactory sut = new(new StringLocalizerMock());

        //  Act
        IInputBuilder result = sut.DateTimeInput("BirthDate", new DateTime(1234, 12, 31, 11, 22, 33));

        //  Assert
        result.Should().Html("""<input id="id_BirthDate" name="BirthDate" type="datetime-local" value="1234-12-31T11:22"></input>""");
    }
}
