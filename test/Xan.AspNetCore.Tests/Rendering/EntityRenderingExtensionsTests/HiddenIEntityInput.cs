using Microsoft.AspNetCore.Html;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.EntityRenderingExtensionsTests;

public class HiddenIEntityInput
{
    [Fact]
    public void ShouldReturnCorrectHtml()
    {
        //  Arrange
        IEntity entity = X.StrictFake<IEntity>();
        A.CallTo(() => entity.Id).Returns(666);
        DefaultHtmlFactory sut = new(new StringLocalizerMock());

        //  Act
        IHtmlContent result = sut.HiddenIEntityInput(entity);

        //  Assert
        result.Should().Html("""<input id="id_Id" name="Id" type="hidden" value="666"></input>""");
    }
}
