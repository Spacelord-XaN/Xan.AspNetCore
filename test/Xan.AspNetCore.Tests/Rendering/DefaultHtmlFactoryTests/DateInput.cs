using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class DateInput
    : TestBase
{
    [Fact]
    public void DateOnly()
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.DateInput("BirthDate", new DateOnly(1234, 12, 31));

        //  Assert
        result.Should().BeHtml("""<input id="id_BirthDate" name="BirthDate" type="date" value="1234-12-31"></input>""");
    }

    [Fact]
    public void DateTime()
    {
        //  Arrange

        //  Act
        IInputBuilder result = Sut.DateInput("BirthDate", new DateTime(1234, 12, 31, 11, 22, 33));

        //  Assert
        result.Should().BeHtml("""<input id="id_BirthDate" name="BirthDate" type="date" value="1234-12-31"></input>""");
    }
}
