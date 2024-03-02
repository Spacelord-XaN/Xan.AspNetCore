using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.ColumnBuilderExtensionsTests;

public class ForEditLink
    : TestBase
{
    private readonly ICrudRouter _router = X.StrictFake<ICrudRouter>();
    [Theory]
    [AutoData]
    public void CrudItemModel(int index, CrudItemModel<TestEntity> item, string url)
    {
        //  Arrange
        ColumnBuilder<CrudItemModel<TestEntity>> Sut = CreateSut<CrudItemModel<TestEntity>>();
        A.CallTo(() => _router.ToEdit(item.Entity.Id, null)).Returns(url);

        //  Act
        ColumnConfig<CrudItemModel<TestEntity>> result = Sut.ForEditLink(_router).Build();

        //  Assert
        result.GetContent(index, item).Should().BeHtml($"""<a href="{url}">Xan_AspNetCore_Edit</a>""");

        A.CallTo(() => _router.ToEdit(item.Entity.Id, null)).MustHaveHappenedOnceExactly();
    }

    [Theory]
    [AutoData]
    public void Entity(int index, TestEntity entity, string url)
    {
        //  Arrange
        ColumnBuilder<TestEntity> Sut = CreateSut<TestEntity>();
        A.CallTo(() => _router.ToEdit(entity.Id, null)).Returns(url);

        //  Act
        ColumnConfig<TestEntity> result = Sut.ForEditLink(_router).Build();

        //  Assert
        result.GetContent(index, entity).Should().BeHtml($"""<a href="{url}">Xan_AspNetCore_Edit</a>""");

        A.CallTo(() => _router.ToEdit(entity.Id, null)).MustHaveHappenedOnceExactly();
    }
}
