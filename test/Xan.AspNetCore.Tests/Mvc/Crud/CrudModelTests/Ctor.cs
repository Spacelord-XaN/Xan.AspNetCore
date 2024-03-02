using Xan.AspNetCore.Mvc.Crud;

namespace Xan.AspNetCore.Tests.Mvc.Crud.CrudModelTests;

public class Ctor
{
    [Theory]
    [AutoData]
    public void ShouldInitProperties(TestEntity entity, string title)
    {
        //  Arrange
        CrudModel<TestEntity>.CreateEditorDelegate createEditor = X.StrictFake<CrudModel<TestEntity>.CreateEditorDelegate>();

        //  Act
        CrudModel<TestEntity> sut = new CrudModel<TestEntity>(entity, createEditor, title);

        //  Assert
        using (new AssertionScope())
        {
            sut.Entity.Should().Be(entity);
            sut.Title.Should().Be(title);
        }
    }
}
