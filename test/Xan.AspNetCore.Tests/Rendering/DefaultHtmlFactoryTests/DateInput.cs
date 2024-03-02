using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.DefaultHtmlFactoryTests;

public class DateInput
{
    [Fact]
    public void DateOnly()
    {
        //  Arrange
        DefaultHtmlFactory sut = new(new StringLocalizerMock());

        //  Act
        IInputBuilder result = sut.DateInput("BirthDate", new DateOnly(1234, 12, 31));

        //  Assert
        result.Should().BeHtml("""<input id="id_BirthDate" name="BirthDate" type="date" value="1234-12-31"></input>""");
    }

    [Fact]
    public void DateTime()
    {
        //  Arrange
        DefaultHtmlFactory sut = new(new StringLocalizerMock());

        //  Act
        IInputBuilder result = sut.DateInput("BirthDate", new DateTime(1234, 12, 31, 11, 22, 33));

        //  Assert
        result.Should().BeHtml("""<input id="id_BirthDate" name="BirthDate" type="date" value="1234-12-31"></input>""");
    }
}
