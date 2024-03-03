using Microsoft.AspNetCore.Mvc.Rendering;
using Xan.AspNetCore.Models;
using Xan.AspNetCore.Mvc.Crud;
using Xan.AspNetCore.Rendering;

namespace Xan.AspNetCore.Tests.Rendering.TableBuilderExtensionsTests;

public class DeleteOrToggleStateLinkColumn
    : TestBase
{
    private readonly ICrudRouter _router = X.StrictFake<ICrudRouter>();

    [Theory]
    [AutoData]
    public void CanDelete(TestEntity item, string url)
    {
        // Arrange
        A.CallTo(() => _router.ToDelete(item.Id)).Returns(url);
        TableBuilder<CrudItemModel<TestEntity>> sut = CreateSut([new CrudItemModel<TestEntity>(item, true)]);

        // Act
        TagBuilder result = sut.DeleteOrToggleStateLinkColumn(_router).Build();

        // Assert
        result.Should().BeHtml($"""<table><thead><tr><th scope="col" style="width: auto;text-align: left;white-space: nowrap;"></th></tr></thead><tbody><tr><td style="width: auto;text-align: left;white-space: nowrap;"><a href="{url}">Xan_AspNetCore_Delete</a></td></tr></tbody></table>""");
    }

    [Theory]
    [AutoData]
    public void CannotDelete_Enabled(TestEntity item, string url)
    {
        // Arrange
        item.State = ObjectState.Enabled;
        A.CallTo(() => _router.ToDisable(item.Id)).Returns(url);
        TableBuilder<CrudItemModel<TestEntity>> sut = CreateSut([new CrudItemModel<TestEntity>(item, false)]);

        // Act
        TagBuilder result = sut.DeleteOrToggleStateLinkColumn(_router).Build();

        // Assert
        result.Should().BeHtml($"""<table><thead><tr><th scope="col" style="width: auto;text-align: left;white-space: nowrap;"></th></tr></thead><tbody><tr><td style="width: auto;text-align: left;white-space: nowrap;"><a href="{url}">Xan_AspNetCore_Disable</a></td></tr></tbody></table>""");
    }

    [Theory]
    [AutoData]
    public void CannotDelete_Disabled(TestEntity item, string url)
    {
        // Arrange
        item.State = ObjectState.Disabled;
        A.CallTo(() => _router.ToEnable(item.Id)).Returns(url);
        TableBuilder<CrudItemModel<TestEntity>> sut = CreateSut([new CrudItemModel<TestEntity>(item, false)]);

        // Act
        TagBuilder result = sut.DeleteOrToggleStateLinkColumn(_router).Build();

        // Assert
        result.Should().BeHtml($"""<table><thead><tr><th scope="col" style="width: auto;text-align: left;white-space: nowrap;"></th></tr></thead><tbody><tr><td style="width: auto;text-align: left;white-space: nowrap;"><a href="{url}">Xan_AspNetCore_Enable</a></td></tr></tbody></table>""");
    }
}
