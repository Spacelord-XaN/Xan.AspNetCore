using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore.Tests.Mvc.Crud.CrudModelTests;

public class EditorAsync
{
    [Theory]
    [AutoData]
    public async Task ShouldInitProperties(TestEntity entity, string title, string htmlString)
    {
        //  Arrange
        ViewContext viewContext = new();
        CrudModel<TestEntity>.CreateEditorDelegate createEditor = X.StrictFake<CrudModel<TestEntity>.CreateEditorDelegate>();
        IHtmlContent htmlContent = new HtmlString(htmlString);
        A.CallTo(() => createEditor.Invoke(viewContext, entity)).Returns(htmlContent);
        CrudModel<TestEntity> sut = new (entity, createEditor, title);

        //  Act
        IHtmlContent result = await sut.EditorAsync(viewContext);

        //  Assert
        result.Should().BeHtml(htmlString);
        A.CallTo(() => createEditor.Invoke(viewContext, entity)).MustHaveHappenedOnceExactly();
    }
}
