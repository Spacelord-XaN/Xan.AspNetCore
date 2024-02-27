using Microsoft.AspNetCore.Html;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.AspNetCore.Rendering;
using Xan.AspNetCore.Tests.Mockups;

namespace Xan.AspNetCore.Tests.Rendering.EntityRenderingExtensionsTests;

public class HiddenIHasTimestampsInput
{
    [Fact]
    public void ShouldReturnCorrectHtml()
    {
        //  Arrange
        IHasTimestamps entity = X.StrictFake<IHasTimestamps>();
        A.CallTo(() => entity.CreatedAt).Returns(new DateTime(2063, 04, 05, 11, 22, 33));
        DefaultHtmlFactory sut = new(new StringLocalizerMock());

        //  Act
        IHtmlContent result = sut.HiddenIHasTimestampsInput(entity);

        //  Assert
        result.Should().Html("""<input id="id_CreatedAt" name="CreatedAt" type="hidden" value="2063-04-05T11:22"></input>""");
    }
}
