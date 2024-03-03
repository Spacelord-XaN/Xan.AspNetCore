using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.TableBuilderExtensionsTests;

public class IdColumn
    : TestBase
{
    [Theory]
    [AutoData]
    public void Entity(TestEntity item)
    {
        // Arrange
        TableBuilder<TestEntity> sut = CreateSut([item]);

        // Act
        TagBuilder result = sut.IdColumn().Build();

        // Assert
        result.Should().BeHtml($"""<table><thead><tr><th scope="col" style="width: auto;text-align: left;white-space: nowrap;">Xan_AspNetCore_Id</th></tr></thead><tbody><tr><td style="width: auto;text-align: left;white-space: nowrap;">{item.Id}</td></tr></tbody></table>""");
    }

    [Theory]
    [AutoData]
    public void CrudItemModel(TestEntity item)
    {
        // Arrange
        TableBuilder<CrudItemModel<TestEntity>> sut = CreateSut([new CrudItemModel<TestEntity>(item, false)]);

        // Act
        TagBuilder result = sut.IdColumn().Build();

        // Assert
        result.Should().BeHtml($"""<table><thead><tr><th scope="col" style="width: auto;text-align: left;white-space: nowrap;">Xan_AspNetCore_Id</th></tr></thead><tbody><tr><td style="width: auto;text-align: left;white-space: nowrap;">{item.Id}</td></tr></tbody></table>""");
    }

    [Theory]
    [AutoData]
    public void Func(TestEntity item)
    {
        // Arrange
        TableBuilder<TestEntity> sut = CreateSut([item]);

        // Act
        TagBuilder result = sut.IdColumn(entity => entity.Id).Build();

        // Assert
        result.Should().BeHtml($"""<table><thead><tr><th scope="col" style="width: auto;text-align: left;white-space: nowrap;">Xan_AspNetCore_Id</th></tr></thead><tbody><tr><td style="width: auto;text-align: left;white-space: nowrap;">{item.Id}</td></tr></tbody></table>""");
    }
}
